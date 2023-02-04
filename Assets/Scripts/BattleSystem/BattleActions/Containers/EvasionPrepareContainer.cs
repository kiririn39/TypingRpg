using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions.Containers
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/EvasionPrepare")]
    public class EvasionPrepareContainer : ActionContainerBase
    {
        [SerializeField] private EvasionPrepareAction evasionEffect;

        private void OnEnable()
        {
            if (evasionEffect != null)
                return;

            evasionEffect = new EvasionPrepareAction();
        }

        public override BattleActionBase CloneAction() => evasionEffect.Clone();
    }
}