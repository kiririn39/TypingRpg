using DefaultNamespace;
using Managers;
using System.Resources;

namespace Assets.Scripts.SkillTree
{
    public class RuneBattleActionInfo
    {
        public string name => ResourcesManager.Instance.getSkillTitle(battleActionBase);
        public BattleActionBase battleActionBase = null;


        public RuneBattleActionInfo(BattleActionBase battleActionBase)
        {
            this.battleActionBase = battleActionBase;
        }
        public override string ToString() => $"{nameof(name)}: {name}, {nameof(battleActionBase)}: ({battleActionBase})";
    }
}
