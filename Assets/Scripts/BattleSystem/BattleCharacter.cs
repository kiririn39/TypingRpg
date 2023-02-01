using System;
using System.Collections.Generic;
using BattleSystem.BattleActions;
using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    public class BattleCharacter : MonoBehaviour
    {
        [SerializeField] [SerializeReference] protected BattleCharacterController controller;
        [SerializeField] [SerializeReference] protected ProgressBar healthBar;
        [SerializeField] [SerializeReference] protected List<ActionModificatorBase> actionModificators;

        public float HealthPoints;
        protected BattleActionBase battleAction;


        private void Awake()
        {
            healthBar.MaxValue = HealthPoints;
        }

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

            healthBar.SetValue(HealthPoints);
        }

        public bool CanInterruptCurrentAction() => battleAction is IInterrruptable;
        public void InterruptCurrentAction() => (battleAction as IInterrruptable)?.Interrupt();
    }
}