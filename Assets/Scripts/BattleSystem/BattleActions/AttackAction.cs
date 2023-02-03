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


        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            float completesAt = InitializationTimestamp + ExecutionDelay;

            Caster.statusBar.SetCurrentDelayNormalized((Time.time - InitializationTimestamp) / ExecutionDelay);
            if (completesAt > Time.time)
                return GameBattleSystem.InProgressAction;

            Caster.playAnimationForBattleActionAsCaster(this);
            foreach (var battleCharacterBase in targets)
            {
                Debug.Log($"Caster {Caster.name} Attacking {battleCharacterBase.name}");
                var physicalAttackDefences =
                    battleCharacterBase.GetActionModificators().OfType<PhysicalAttackDefence>();
                var attackPoint = AttackPoints;

                foreach (var attackDefence in physicalAttackDefences)
                    attackPoint *= attackDefence.defencePercentage;

                battleCharacterBase.DealDamage(attackPoint, GetType());
                battleCharacterBase.playAnimationForBattleActionAsTarget(this);
            }

            return GameBattleSystem.FinishedAction;
        }

        public override BattleActionBase Clone()
        {
            return new AttackAction
            {
                Caster = base.Caster,
                AttackPoints = this.AttackPoints,
                ExecutionDelay = this.ExecutionDelay,
            };
        }

        private void UpdateCharacterStatusBar(BattleCharacter character, float normalizedValue)
        {
        }
    }
}