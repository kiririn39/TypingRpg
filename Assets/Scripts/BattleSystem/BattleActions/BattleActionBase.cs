using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public abstract class BattleActionBase
    {
        [HideInInspector] public BattleCharacterBase Caster;

        public abstract bool ExecuteAction(List<BattleCharacterBase> targets);
        public abstract BattleActionBase Clone();
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