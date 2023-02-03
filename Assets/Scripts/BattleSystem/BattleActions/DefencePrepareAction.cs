using System;
using System.Collections.Generic;
using BattleSystem.BattleActions;
using UnityEngine;

namespace DefaultNamespace.BattleActions
{
    [Serializable]
    public class DefencePrepareAction : BattleActionBase, ITargetsSelf
    {
        public float ExecutionDelay;
        public DefenceEffect DefenceEffect;


        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            float completesAt = InitializationTimestamp + ExecutionDelay;

            Caster.statusBar.SetCurrentDelayNormalized((Time.time - InitializationTimestamp) / ExecutionDelay);
            if (completesAt > Time.time)
                return GameBattleSystem.InProgressAction;

            var defenceEffect = DefenceEffect.Clone();
            defenceEffect.Initialize(Caster);

            return new FinishedWithEffectActionResult
            {
                effect = defenceEffect
            };
        }

        public override BattleActionBase Clone()
        {
            var result = new DefencePrepareAction
            {
                ExecutionDelay = ExecutionDelay,
                DefenceEffect = DefenceEffect.Clone() as DefenceEffect
            };

            return result;
        }
    }
}