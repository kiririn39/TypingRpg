using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BattleCharacterBase : MonoBehaviour
    {
        public float DamagePhysic     { get; private set; }
        public float DamageMagic      { get; private set; }
        public float HealthMax        { get; private set; }
        public float HealthCur        { get; private set; }
        public byte  CritStrikeChance { get; private set; }
        public float ArmorPhysic      { get; private set; }
        public float ArmorMagical     { get; private set; }
        public int   CooldownReduce   { get; private set; }

        public abstract BattleActionBase GetNextAction();
    }
}
