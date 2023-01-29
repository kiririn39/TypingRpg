using DefaultNamespace.BattleActions;
using DefaultNamespace.Context;
using System;
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


        public abstract BattleActionBase GetNextAction();

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
    }
}
