using DefaultNamespace.BattleActions;
using DefaultNamespace.Context;
using System;
using System.Collections.Generic;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BattleCharacterBase : MonoBehaviour
    {
        public float DamagePhysical   { get; private set; }
        public float DamageMagical    { get; private set; }
        public float HealthMax        { get; private set; }
        public float HealthCur        { get; private set; }
        public byte  CritStrikeChance { get; private set; }
        public float ArmorPhysical    { get; private set; }
        public float ArmorMagical     { get; private set; }
        public int   CooldownReduce   { get; private set; }
        
        protected BattleActionBase battleAction;

        [SerializeField] protected List<BattleActionBase> actions = new List<BattleActionBase>{new AttackAction()};

        public abstract BattleActionBase GetAction();
        public abstract BattleActionBase GenerateNextAction();
        public bool CanInterruptCurrentAction() => battleAction is IInterrruptable;

        public virtual void InterruptCurrentAction() => (battleAction as IInterrruptable)?.Interrupt();
    }
}