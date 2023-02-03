using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions
{
    [Serializable]
    public class HealPrepareAction : BattleActionBase, ITargetsSelf
    {
        public float ExecutionDelay;
        public HealEffect HealEffect;


        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            float completesAt = InitializationTimestamp + ExecutionDelay;

            Caster.statusBar.SetCurrentDelayNormalized((Time.time - InitializationTimestamp) / ExecutionDelay);
            if (completesAt > Time.time)
                return GameBattleSystem.InProgressAction;

            var healEffect = HealEffect.Clone();
            healEffect.Initialize(Caster);

            return new FinishedWithEffectActionResult
            {
                effect = healEffect
            };
        }

        public override BattleActionBase Clone()
        {
            var result = new HealPrepareAction()
            {
                ExecutionDelay = ExecutionDelay,
                HealEffect = HealEffect.Clone() as HealEffect
            };

            return result;
        }
    }
}