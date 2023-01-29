using System.Collections.Generic;

namespace DefaultNamespace
{
    public abstract class BattleActionBase
    {
        private BattleCharacterBase caster = null;
        private List<BattleCharacterBase> targets = null;


        public virtual void Initialize(BattleCharacterBase caster, List<BattleCharacterBase> targets)   
        {
            this.caster = caster;
            this.targets = targets;
        }
    }
}