using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/FireAttack")]
    public class FireAttackContainer : ActionContainerBase
    {
        [SerializeField] private MagicFireAction action;

        private void OnEnable()
        {
            if (action != null)
                return;

            action = new MagicFireAction();
        }

        public override BattleActionBase CloneAction() => action.Clone();
    }
}