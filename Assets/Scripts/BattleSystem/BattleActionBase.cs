using System.Collections.Generic;

namespace DefaultNamespace
{
    public abstract class BattleActionBase
    {
        public abstract void Initialize(BattleCharacterBase caster, List<BattleCharacterBase> targets);
    }
}