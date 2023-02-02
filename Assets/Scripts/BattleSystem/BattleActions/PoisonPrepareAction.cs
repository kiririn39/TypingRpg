using System;
using System.Collections.Generic;
using BattleSystem.BattleActions;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class PoisonPrepareAction : BattleActionBase, ITargetsSelf
    {
        public float ExecutionDelay;
        public PoisonEffect PoisonEffect;


        public override ActionResultBase ExecuteAction(List<BattleCharacter> targets)
        {
            float completesAt = InitializationTimestamp + ExecutionDelay;

            Caster.statusBar.SetCurrentDelayNormalized((Time.time - InitializationTimestamp) / ExecutionDelay);
            if (completesAt > Time.time)
                return GameBattleSystem.InProgressAction;

            var poisonEffect = PoisonEffect.Clone();
            poisonEffect.Initialize(Caster);

            return new FinishedWithEffectActionResult
            {
                effect = poisonEffect
            };
        }

        public override BattleActionBase Clone()
        {
            var result = new PoisonPrepareAction
            {
                ExecutionDelay = ExecutionDelay,
                PoisonEffect = PoisonEffect.Clone() as PoisonEffect
            };

            return result;
        }
    }
}