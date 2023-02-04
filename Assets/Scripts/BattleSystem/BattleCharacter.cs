using System.Collections.Generic;
using BattleSystem.BattleActions;
using Common;
using DefaultNamespace.BattleActions;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class BattleCharacter : MonoBehaviour
    {
        public event Action onInit = delegate {};

        public event Action<float, float> onHealthChanged = delegate {};
        public event Action<float, float> onMaxHealthChanged = delegate {};
        public event Action<float, float> onDelayNormalizedChanged = delegate {};


        [FormerlySerializedAs("controller")]
        [SerializeField] [SerializeReference] public Transform trHealthBarPlace;
        [SerializeField] [SerializeReference] public BattleCharacterControllerBase controllerBase;
        [SerializeField] [SerializeReference] public List<ActionModificatorBase> actionModificators;
        [SerializeField] [SerializeReference] protected BattleCharacterAnimator battleCharacterAnimator;

        [SerializeField]private float _MaxHealthPoints = 10f;
        public float MaxHealthPoints
        {
            get => _MaxHealthPoints;
            set
            {
                float oldValue = _MaxHealthPoints;
                _MaxHealthPoints = value.withMin(0);
                onMaxHealthChanged(oldValue, _MaxHealthPoints);
            }
        }

        [SerializeField]private float _HealthPoints = 0;
        public float HealthPoints
        {
            get => _HealthPoints;
            set
            {
                float oldValue = _HealthPoints;
                _HealthPoints = value.withMin(0).withMax(MaxHealthPoints);
                onHealthChanged(oldValue, _HealthPoints);

                if (_HealthPoints == 0)
                    battleCharacterAnimator.play(BattleCharacterAnimator.AnimationType.DEATH);
            }
        }

        private float _DelayNormalized = 0;
        public float DelayNormalized
        {
            get => _DelayNormalized;
            set
            {
                float oldValue = _DelayNormalized;
                _DelayNormalized = value.withMin(0).withMax(1);
                onDelayNormalizedChanged(oldValue, _DelayNormalized);
            }
        }

        protected BattleActionBase battleAction;


        private void Awake()
        {
            _HealthPoints = MaxHealthPoints;
            _DelayNormalized = 1;
            
            //TODO
            battleCharacterAnimator.init(controllerBase is PlayerCharacterController ? BattleCharacterAnimator.Character.PLAYER : BattleCharacterAnimator.Character.MAG);

            onInit();
        }

        public BattleActionBase GetAction()
        {
            if (battleAction == null)
                GenerateNextAction();

            return battleAction;
        }

        public void GenerateNextAction()
        {
            battleAction = controllerBase.GenerateNextAction();
        }

        public void DealDamage(float value, Type battleActionType )
        {
            HealthPoints -= value;
        }

        public void Heal(float value, Type battleActionType )
        {
            HealthPoints += value;
        }

        public void playAnimation(BattleCharacterAnimator.AnimationType animationType)
        {
            battleCharacterAnimator.play(animationType);
        }

        public void playAnimationForBattleActionAsTarget(BattleActionBase battleActionBase)
        {
            if (HealthPoints <= 0)
            {
                battleCharacterAnimator.play(BattleCharacterAnimator.AnimationType.DEATH);
                return;
            }

            var animationType = battleActionBase switch
            {
              MagicFireAction  magicFireAction  => BattleCharacterAnimator.AnimationType.TAKE_DAMAGE
            , MagicFrostAction magicFrostAction => BattleCharacterAnimator.AnimationType.TAKE_DAMAGE
            , PoisonEffect     poisonEffect     => BattleCharacterAnimator.AnimationType.TAKE_DAMAGE_POISON
            , PsyonicAction    psyonicAction    => BattleCharacterAnimator.AnimationType.TAKE_DAMAGE
            , AttackAction     attackAction     => BattleCharacterAnimator.AnimationType.TAKE_DAMAGE

            , _ => battleActionBase is ITargetsSelf ? BattleCharacterAnimator.AnimationType.IDLE : BattleCharacterAnimator.AnimationType.TAKE_DAMAGE
            };

            battleCharacterAnimator.play(animationType);
        }

        public void playAnimationForBattleActionAsCaster(BattleActionBase battleActionBase)
        {
            var animationType = battleActionBase switch
            {
              MagicFireAction     magicFireAction     => BattleCharacterAnimator.AnimationType.ATTACK_FIRE
            , MagicFrostAction    magicFrostAction    => BattleCharacterAnimator.AnimationType.ATTACK_FROST
            , PsyonicAction       psyonicAction       => BattleCharacterAnimator.AnimationType.ATTACK_PSYONIC
            , AttackAction        attackAction        => BattleCharacterAnimator.AnimationType.ATTACK
            , DefencePrepareAction       defenceAction       => BattleCharacterAnimator.AnimationType.DEFENCE
            , IdleAction          idleAction          => BattleCharacterAnimator.AnimationType.IDLE
            , PoisonPrepareAction poisonPrepareAction => BattleCharacterAnimator.AnimationType.ATTACK_POISON

            , _ => battleActionBase is ITargetsSelf ? BattleCharacterAnimator.AnimationType.IDLE : BattleCharacterAnimator.AnimationType.ATTACK
            };

            battleCharacterAnimator.play(animationType);
        }

        public bool CanInterruptCurrentAction() => battleAction is IInterrruptable;
        public void InterruptCurrentAction() => (battleAction as IInterrruptable)?.Interrupt();
    }
}