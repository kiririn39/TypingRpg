using DefaultNamespace;
using DefaultNamespace.BattleActions;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/DefencePrepare")]
    public class DefenceActionContainer : ActionContainerBase
    {
        [SerializeField] private DefencePrepareAction prepareAction;

        private void OnEnable()
        {
            if (prepareAction != null)
                return;

            prepareAction = new DefencePrepareAction();
        }

        public override BattleActionBase CloneAction() => prepareAction.Clone();
    }
}