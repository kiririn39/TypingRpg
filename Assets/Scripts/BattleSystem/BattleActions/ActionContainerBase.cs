using DefaultNamespace;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace BattleSystem.BattleActions
{
    public abstract class ActionContainerBase : ScriptableObject
    {
        [SerializeField] [SerializeReference] protected BattleActionBase action;

        public BattleActionBase CloneAction() => action.Clone();
    }

    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/AttackAction")]
    public class AttackActionContainer : ActionContainerBase
    {
        private void OnEnable() => action = new AttackAction();
    }

    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/DefenceAction")]
    public class DefenceActionContainer : ActionContainerBase
    {
        private void OnEnable() => action = new DefenceAction();
    }

    [CreateAssetMenu(menuName = "BattleSystem/ActionContainer/IdleAction")]
    public class IdleActionContainer : ActionContainerBase
    {
        private void OnEnable() => action = new IdleAction();
    }
}