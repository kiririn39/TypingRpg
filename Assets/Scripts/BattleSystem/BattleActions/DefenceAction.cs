using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class DefenceAction : BattleActionBase, ITargetsSelf, IInterrruptable
    {
        [SerializeField] private float DefencePoints;

        public override bool ExecuteAction(List<BattleCharacterBase> targets)
        {
            foreach (var battleCharacterBase in targets)
            {
                //battleCharacterBase.DamagePhysical += DefencePoints
            }

            throw new System.NotImplementedException();
        }

        public void Interrupt()
        {
            throw new System.NotImplementedException();
        }
    }
}