using DefaultNamespace;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/AttackAction")]
    public class AttackActionContainer : ActionContainerBase
    {
        [SerializeField] private AttackAction action;

        private void OnEnable()
        {
            if (action != null)
                return;

            action = new AttackAction();
        }

        public override BattleActionBase CloneAction() => action.Clone();
    }
}