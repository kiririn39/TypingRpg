using DefaultNamespace;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.SkillTree
{
    public class RuneNodeData
    {
        private Guid Guid = Guid.NewGuid();

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
        public override int GetHashCode() => Guid.GetHashCode();

        public override string ToString() => $"{nameof(runeKey)}: {runeKey}, {nameof(RuneBattleActionInfo)}: {RuneBattleActionInfo}, {nameof(Guid)}: {Guid}";
    }
}
