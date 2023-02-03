using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleActions;
using Common;
using UnityEngine;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class AttackAction : BattleActionBase, ITargetsOpposingCharacter
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

            Caster.statusBar.SetCurrentDelayNormalized((Time.time - InitializationTimestamp) / ExecutionDelay);

            if (damageInflictedTime < Time.time && !isDamageInflicted)
            {
                foreach (var battleCharacterBase in targets)
                {
                    Debug.Log($"Caster {Caster.name} Attacking {battleCharacterBase.name}");
                    var physicalAttackDefences =
                        battleCharacterBase.GetActionModificators().OfType<PhysicalAttackDefence>();
                    var attackPoint = AttackPoints;

                    foreach (var attackDefence in physicalAttackDefences)
                        attackPoint *= attackDefence.defencePercentage;

                    battleCharacterBase.DealDamage(attackPoint, GetType());
                    battleCharacterBase.playAnimation(BattleCharacterAnimator.AnimationType.TAKE_DAMAGE);
                }

                isDamageInflicted = true;
            }

            if (completesAt > Time.time)
                return GameBattleSystem.InProgressAction;

            return GameBattleSystem.FinishedAction;
        }

        public override BattleActionBase Clone()
        {
            return new AttackAction
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