using DefaultNamespace;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.SkillTree
{
    public class RuneNodeData
    {
        public RuneKey runeKey = RuneKey.NONE;
        public RuneBattleActionInfo RuneBattleActionInfo;

        protected bool Equals(RuneNodeData other) => runeKey == other.runeKey && Equals(RuneBattleActionInfo, other.RuneBattleActionInfo);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((RuneNodeData)obj);
        }
        public override int GetHashCode() => HashCode.Combine((int)runeKey, RuneBattleActionInfo);
        public override string ToString() => $"{nameof(runeKey)}: {runeKey.ToString()}, {nameof(RuneBattleActionInfo)}: {RuneBattleActionInfo}";
    }
}
