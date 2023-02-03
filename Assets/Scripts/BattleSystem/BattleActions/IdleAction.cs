using System;
using System.Collections.Generic;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class IdleAction : BattleActionBase, ITargetsSelf, IInterrruptable
    {
        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            Caster.playAnimation(BattleCharacterAnimator.AnimationType.IDLE);
            return GameBattleSystem.FinishedAction;
        }

        public void Interrupt()
        {
            throw new System.NotImplementedException();
        }

        public override BattleActionBase Clone() => new IdleAction();
    }
}