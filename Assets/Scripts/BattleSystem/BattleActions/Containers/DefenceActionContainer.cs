using DefaultNamespace;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/DefenceAction")]
    public class DefenceActionContainer : ActionContainerBase
    {
        [SerializeField] private DefenceAction action;

        private void OnEnable()
        {
            if (action != null)
                return;

            action = new DefenceAction();
        }

        public override BattleActionBase CloneAction() => action.Clone();
    }
}