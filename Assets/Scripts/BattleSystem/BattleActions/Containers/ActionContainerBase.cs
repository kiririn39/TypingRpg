using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    public abstract class ActionContainerBase : ScriptableObject
    {
        public abstract BattleActionBase CloneAction();
    }
}