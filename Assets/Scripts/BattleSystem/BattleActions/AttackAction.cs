using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class AttackAction : BattleActionBase, ITargetsOpposingCharacter
    {
        [SerializeField] private float AttackPoints;
        [SerializeField] private float ExecutionDelay;


        public override bool ExecuteAction(List<BattleCharacter> targets)
        {
            if (InitializationTimestamp + ExecutionDelay > Time.time)
                return false;

            foreach (var battleCharacterBase in targets)
            {
                Debug.Log($"Caster {Caster} Attacking {battleCharacterBase}");
                battleCharacterBase.DealDamage(AttackPoints);
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