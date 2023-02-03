using System.Collections.Generic;
using BattleSystem.BattleActions;
using DefaultNamespace.BattleActions;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class BattleCharacter : MonoBehaviour
    {
        [FormerlySerializedAs("controller")]
        [SerializeField] [SerializeReference] protected BattleCharacterControllerBase controllerBase;
        [SerializeField] [SerializeReference] public CharacterStatusBar statusBar;
        [SerializeField] [SerializeReference] protected List<ActionModificatorBase> actionModificators;
        [SerializeField] [SerializeReference] protected BattleCharacterAnimator battleCharacterAnimator;

        public float MaxHealthPoints = 10f;
        public float HealthPoints { get; private set; }
        protected BattleActionBase battleAction;


        private void Awake()
        {
            HealthPoints = MaxHealthPoints;
            statusBar.SetMaxHealth(HealthPoints);
            
            //TODO
            battleCharacterAnimator.init(controllerBase is PlayerCharacterController ? BattleCharacterAnimator.Character.PLAYER : BattleCharacterAnimator.Character.MAG);
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

        public List<ActionModificatorBase> GetActionModificators() => actionModificators;

        public void DealDamage(float value, Type battleActionType )
        {
            HealthPoints -= value;

            statusBar.SetCurrentHealth(HealthPoints);
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
            , PoisonEffect        poisonEffect        => BattleCharacterAnimator.AnimationType.ATTACK_POISON
            , PsyonicAction       psyonicAction       => BattleCharacterAnimator.AnimationType.ATTACK_PSYONIC
            , AttackAction        attackAction        => BattleCharacterAnimator.AnimationType.ATTACK
            , DefenceAction       defenceAction       => BattleCharacterAnimator.AnimationType.DEFENCE
            , IdleAction          idleAction          => BattleCharacterAnimator.AnimationType.IDLE
            , PoisonPrepareAction poisonPrepareAction => BattleCharacterAnimator.AnimationType.ATTACK_POISON //???? TODO check is correct

            , _ => battleActionBase is ITargetsSelf ? BattleCharacterAnimator.AnimationType.IDLE : BattleCharacterAnimator.AnimationType.ATTACK
            };

            battleCharacterAnimator.play(animationType);
        }

        public bool CanInterruptCurrentAction() => battleAction is IInterrruptable;
        public void InterruptCurrentAction() => (battleAction as IInterrruptable)?.Interrupt();
    }
}