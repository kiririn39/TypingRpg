using System.Collections.Generic;
using BattleSystem.BattleActions;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BattleCharacterBase : MonoBehaviour
    {
        [SerializeField] [SerializeReference] protected List<ActionModificatorBase> actionModificators;

        public float HealthPoints;
        protected BattleActionBase battleAction;


        public abstract BattleActionBase GetAction();
        public abstract BattleActionBase GenerateNextAction();
        public List<ActionModificatorBase> GetActionModificators() => actionModificators;

        public virtual void DealDamage(float value)
        {
            HealthPoints -= value;
        }

        public bool CanInterruptCurrentAction() => battleAction is IInterrruptable;
        public virtual void InterruptCurrentAction() => (battleAction as IInterrruptable)?.Interrupt();
    }
}