using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/PenetratingAttack")]
    public class PenetratingAttackContainer : ActionContainerBase
    {
        [SerializeField] private PenetratingAttackAction action;

        private void OnEnable()
        {
            if (action != null)
                return;

            action = new PenetratingAttackAction();
        }

        public override BattleActionBase CloneAction() => action.Clone();
    }
}