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

        public virtual BattleActionResult ApplyBattleAction(BattleActionBase battleActionBase)
        {
            BattleActionResult battleActionResult = new BattleActionResult();
            switch (battleActionBase)
            {
            case IdleAction idleAction:
                break;
            case PhysicAttackAction physicAttackAction:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(battleActionBase));

            }

            return battleActionResult;
        }

        public abstract BattleActionBase GetAction();
        public abstract BattleActionBase GenerateNextAction();
        public bool CaInterruptCurrentAction() => battleAction is IInterrruptable;

        public virtual void InterruptCurrentAction() => (battleAction as IInterrruptable)?.Interrupt();
    }
}