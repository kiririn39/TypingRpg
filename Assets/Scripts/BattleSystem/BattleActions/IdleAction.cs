using System;
using System.Collections.Generic;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class IdleAction : BattleActionBase, ITargetsSelf, IInterrruptable
    {
        public override bool ExecuteAction(List<BattleCharacterBase> targets)
        {
            throw new System.NotImplementedException();
        }

        public void Interrupt()
        {
            throw new System.NotImplementedException();
        }

        public override BattleActionBase Clone() => new IdleAction();
    }
}