using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/PoisonPrepare")]
    public class PoisonPrepareContainer : ActionContainerBase
    {
        [SerializeField] private PoisonPrepareAction poisonEffect;

        private void OnEnable()
        {
            if (poisonEffect != null)
                return;

            poisonEffect = new PoisonPrepareAction();
        }

        public override BattleActionBase CloneAction() => poisonEffect.Clone();
    }
}