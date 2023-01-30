using DefaultNamespace;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/IdleAction")]
    public class IdleActionContainer : ActionContainerBase
    {
        public override BattleActionBase CloneAction() => new IdleAction();
    }
}