using System.Collections.Generic;

namespace Assets.Scripts.SkillTree
{
    public class RuneSentenceForBattleAction
    {
        public List<RuneKey> RuneKeys = new List<RuneKey>();
        public RuneBattleActionInfo RuneBattleActionInfo = null;


        public override string ToString() => $"{nameof(RuneKeys)}: ({string.Join(",", RuneKeys)}), {nameof(RuneBattleActionInfo)}: ({RuneBattleActionInfo})";
    }
}
