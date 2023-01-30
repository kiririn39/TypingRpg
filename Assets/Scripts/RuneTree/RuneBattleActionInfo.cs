using DefaultNamespace;

namespace Assets.Scripts.SkillTree
{
    public class RuneBattleActionInfo
    {
        public string name = "";
        public BattleActionBase battleActionBase = null;


        public override string ToString() => $"{nameof(name)}: {name}, {nameof(battleActionBase)}: ({battleActionBase})";
    }
}
