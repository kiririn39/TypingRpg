using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/HealPrepare")]
    public class HealPrepareContainer : ActionContainerBase
    {
        [SerializeField] private HealPrepareAction healEffect;

        private void OnEnable()
        {
            if (healEffect != null)
                return;

            healEffect = new HealPrepareAction();
        }

        public override BattleActionBase CloneAction() => healEffect.Clone();
    }
}