using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class AttackAction : BattleActionBase, ITargetsOpposingCharacter
    {
        [SerializeField] private float AttackPoints;

        public override bool ExecuteAction(List<BattleCharacterBase> targets)
        {
            foreach (var battleCharacterBase in targets)
            {
                //battleCharacterBase. -= AttackPoints;
            }

            return true;
        }

        public override BattleActionBase Clone()
        {
            return new AttackAction
            {
                Caster = base.Caster,
                AttackPoints = this.AttackPoints
            };
        }
    }
}