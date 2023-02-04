using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.BattleActions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleSystem.BattleActions
{
    [Serializable]
    public class PenetratingAttackAction : BattleActionBase, ITargetsOpposingCharacter
    {
        public float AttackPoints;
        public float ExecutionDelay;
        public float DamageInflictedTime;

        private bool isDamageInflicted = false;

        public override void Initialize(BattleCharacter Caster)
        {
            base.Initialize(Caster);

            Caster.playAnimation(BattleCharacterAnimator.AnimationType.ATTACK);
            isDamageInflicted = false;
        }

        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            float completesAt = InitializationTimestamp + ExecutionDelay;
            float damageInflictedTime = InitializationTimestamp + DamageInflictedTime;

            Caster.DelayNormalized = (Time.time - InitializationTimestamp) / ExecutionDelay;

            if (damageInflictedTime < Time.time && !isDamageInflicted)
            {
                foreach (var battleCharacter in targets)
                {
                    var evasions = battleCharacter.actionModificators.OfType<PhysicalEvasion>();
                    bool hasEvaded = false;

                    foreach (var evasion in evasions)
                    {
                        hasEvaded = evasion.EvasionRate >= Random.value;
                        if (!hasEvaded)
                            continue;

                        break;
                    }

                    if (hasEvaded)
                    {
                        Debug.Log($"{battleCharacter.name} evaded {Caster.name}'s attack ");
                        break;
                        // play defefnce animation or whatever 
                    }

                    battleCharacter.DealDamage(AttackPoints, GetType());
                    battleCharacter.playAnimation(BattleCharacterAnimator.AnimationType.TAKE_DAMAGE);
                    SoundManager.Instance.playSound(SoundType.BASIC_ATTACK);
                }

                isDamageInflicted = true;
            }

            if (completesAt > Time.time)
                return GameBattleSystem.InProgressAction;

            return GameBattleSystem.FinishedAction;
        }

        public override BattleActionBase Clone()
        {
            return new PenetratingAttackAction
            {
                Caster = base.Caster,
                AttackPoints = this.AttackPoints,
                ExecutionDelay = this.ExecutionDelay,
                DamageInflictedTime = this.DamageInflictedTime,
            };
        }

        private void UpdateCharacterStatusBar(BattleCharacter character, float normalizedValue)
        {
        }
    }
}