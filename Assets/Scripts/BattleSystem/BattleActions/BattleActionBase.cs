using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    [Serializable]
    public abstract class BattleActionBase
    {
        protected BattleCharacterBase Caster;

        public BattleCharacterBase GetCaster => Caster;

        public void Initialize(BattleCharacterBase caster)
        {
            Caster = caster;
        }

        public abstract bool ExecuteAction(List<BattleCharacterBase> targets);
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