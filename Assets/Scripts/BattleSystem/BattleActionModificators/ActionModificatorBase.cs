using System;
using DefaultNamespace;

namespace BattleSystem.BattleActions
{
    [Serializable]
    public abstract class ActionModificatorBase
    {
        public abstract void ModifyAction(BattleActionBase action);
    }
}