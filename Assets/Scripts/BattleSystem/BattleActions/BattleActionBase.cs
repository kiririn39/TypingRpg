using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class BattleActionBase
    {
        [HideInInspector] public BattleCharacter Caster;
        protected float InitializationTimestamp;

        public virtual void Initialize(BattleCharacter Caster)
        {
            this.Caster = Caster;
            InitializationTimestamp = Time.time;
        }

        public virtual bool ExecuteAction(List<BattleCharacter> targets)
        {
            return false;
        }

        public virtual BattleActionBase Clone()
        {
            return null;
        }

        public override string ToString() =>
            $"{nameof(Caster)}: {Caster?.GetType().Name}, {nameof(InitializationTimestamp)}: {InitializationTimestamp}";
    }

    public interface ITargetsSelf
    {
    }

    public interface ITargetsOpposingCharacter
    {
    }

    public interface IInterrruptable
    {
        public void Interrupt();
    }
}