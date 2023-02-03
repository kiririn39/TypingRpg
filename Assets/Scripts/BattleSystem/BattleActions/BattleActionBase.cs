using Common;
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

        public ActionResultBase ExecuteAction(List<BattleCharacter> targets)
        {
            ActionResultBase result = ExecuteActionImpl(targets);
            return result;
        }

        protected virtual ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            return GameBattleSystem.FinishedAction;
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

    public interface IEffect
    {
        void SetAllEffectsLookup(List<BattleActionBase> battleEffects);
    }
}