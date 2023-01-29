using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class AttackAction : BattleActionBase, ITargetsOpposingCharacter
    {
        [SerializeField] private float AttackPoints;
        [SerializeField] private DamageType damageType;


        public override bool ExecuteAction(List<BattleCharacterBase> targets)
        {
            throw new System.NotImplementedException();
        }
    }
}