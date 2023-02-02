using System;
using System.Collections.Generic;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class IdleAction : BattleActionBase, ITargetsSelf, IInterrruptable
    {
        public override ActionResultBase ExecuteAction(List<BattleCharacter> targets)
        {
            return GameBattleSystem.InProgressAction;
        }

        public void Interrupt()
        {
            throw new System.NotImplementedException();
        }

        public override BattleActionBase Clone() => new IdleAction();
    }
}