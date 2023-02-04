using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions
{
    [Serializable]
    public class EvasionPrepareAction : BattleActionBase, ITargetsSelf
    {
        public float ExecutionDelay;
        public EvasionEffect EvasionEffect;


        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            float completesAt = InitializationTimestamp + ExecutionDelay;

            Caster.DelayNormalized = (Time.time - InitializationTimestamp) / ExecutionDelay;
            if (completesAt > Time.time)
                return GameBattleSystem.InProgressAction;

            var evasionEffect = EvasionEffect.Clone();
            evasionEffect.Initialize(Caster);

            return new FinishedWithEffectActionResult
            {
                effect = evasionEffect
            };
        }

        public override BattleActionBase Clone()
        {
            var result = new EvasionPrepareAction
            {
                ExecutionDelay = ExecutionDelay,
                EvasionEffect = EvasionEffect.Clone() as EvasionEffect
            };

            return result;
        }
    }
}