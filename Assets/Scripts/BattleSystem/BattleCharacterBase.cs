using System.Collections.Generic;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BattleCharacterBase : MonoBehaviour
    {
        protected float Hitpoints;
        protected float Defence;
        protected BattleActionBase battleAction;

        [SerializeField] protected List<BattleActionBase> actions = new List<BattleActionBase>{new AttackAction()};


        public abstract BattleActionBase GetAction();
        public abstract BattleActionBase GenerateNextAction();
        public bool CaInterruptCurrentAction() => battleAction is IInterrruptable;

        public virtual void InterruptCurrentAction() => (battleAction as IInterrruptable)?.Interrupt();
    }
}