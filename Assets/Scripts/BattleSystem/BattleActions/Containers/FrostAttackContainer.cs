using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/FrostAttack")]
    public class FrostAttackContainer : ActionContainerBase
    {
        [SerializeField] private MagicFrostAction action;

        private void OnEnable()
        {
            if (action != null)
                return;

            action = new MagicFrostAction();
        }

        public override BattleActionBase CloneAction() => action.Clone();
    }
}