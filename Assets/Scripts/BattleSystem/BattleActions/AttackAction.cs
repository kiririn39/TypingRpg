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


        public override bool ExecuteAction(List<BattleCharacter> targets)
        {
            if (InitializationTimestamp + ExecutionDelay > Time.time)
                return false;

            foreach (var battleCharacterBase in targets)
            {
                Debug.Log($"Caster {Caster} Attacking {battleCharacterBase}");
                var physicalAttackDefences =
                    battleCharacterBase.GetActionModificators().OfType<PhysicalAttackDefence>();
                var attackPoint = AttackPoints;

                foreach (var attackDefence in physicalAttackDefences)
                    attackPoint *= attackDefence.defencePercentage;

                battleCharacterBase.DealDamage(attackPoint);
            }

            return true;
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
    }
}