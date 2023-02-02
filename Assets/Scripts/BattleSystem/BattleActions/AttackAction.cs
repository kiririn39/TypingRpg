using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleActions;
using UnityEngine;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class AttackAction : BattleActionBase, ITargetsOpposingCharacter
    {
        public float AttackPoints;
        public float ExecutionDelay;


        public override ActionResultBase ExecuteAction(List<BattleCharacter> targets)
        {
            float completesAt = InitializationTimestamp + ExecutionDelay;

            Caster.statusBar.SetCurrentDelayNormalized((Time.time - InitializationTimestamp) / ExecutionDelay);
            if (completesAt > Time.time)
                return GameBattleSystem.InProgressAction;

            foreach (var battleCharacterBase in targets)
            {
                Debug.Log($"Caster {Caster.name} Attacking {battleCharacterBase.name}");
                var physicalAttackDefences =
                    battleCharacterBase.GetActionModificators().OfType<PhysicalAttackDefence>();
                var attackPoint = AttackPoints;

                foreach (var attackDefence in physicalAttackDefences)
                    attackPoint *= attackDefence.defencePercentage;

                battleCharacterBase.DealDamage(attackPoint);
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