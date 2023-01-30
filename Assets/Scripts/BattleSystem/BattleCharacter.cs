using System.Collections.Generic;
using BattleSystem.BattleActions;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace DefaultNamespace
{
    public class BattleCharacter : MonoBehaviour
    {
        [SerializeField] [SerializeReference] protected BattleCharacterController controller;
        [SerializeField] [SerializeReference] protected List<ActionModificatorBase> actionModificators;

        public float HealthPoints;
        protected BattleActionBase battleAction;


        public BattleActionBase GetAction()
        {
            if (battleAction == null)
                GenerateNextAction();

            return battleAction;
        }

        public void GenerateNextAction()
        {
            battleAction = controller.GenerateNextAction();
        }

        public List<ActionModificatorBase> GetActionModificators() => actionModificators;

        public void DealDamage(float value)
        {
            HealthPoints -= value;
        }

        public bool CanInterruptCurrentAction() => battleAction is IInterrruptable;
        public void InterruptCurrentAction() => (battleAction as IInterrruptable)?.Interrupt();
    }
}