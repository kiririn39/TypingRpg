using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions
{
    public class HealEffect : BattleActionBase, IEffect, ITargetsSelf
    {
        public float HealPoints;
        public float HealsCount;
        public float DelayBetweenHeals;
        [HideInInspector] public float LastHealTimestamp;

        private List<BattleActionBase> _battleEffects = null;


        public override void Initialize(BattleCharacter caster)
        {
            base.Initialize(caster);
            LastHealTimestamp = InitializationTimestamp;
        }

        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            if (HealsCount <= 0)
                return GameBattleSystem.FinishedAction;
            if (LastHealTimestamp + DelayBetweenHeals > Time.time)
                return GameBattleSystem.InProgressAction;

            var mostPowerfulHeal = _battleEffects.OfType<HealEffect>()
                .Where(action => action.Caster == this.Caster)
                .OrderByDescending(action => action.HealPoints).FirstOrDefault();

            if (mostPowerfulHeal == this)
                foreach (var character in targets)
                {
                    character.Heal(HealPoints, GetType());
                    Debug.Log($"Caster {Caster.name} Healing self");
                }

            LastHealTimestamp = Time.time;
            HealsCount--;

            return HealsCount <= 0 ? GameBattleSystem.FinishedAction : GameBattleSystem.InProgressAction;
        }

        public override BattleActionBase Clone()
        {
            var result = new HealEffect()
            {
                HealPoints = this.HealPoints,
                HealsCount = this.HealsCount,
                DelayBetweenHeals = this.DelayBetweenHeals,
                LastHealTimestamp = this.LastHealTimestamp,
                Caster = this.Caster,
                InitializationTimestamp = this.InitializationTimestamp
            };

            return result;
        }

        public void SetAllEffectsLookup(List<BattleActionBase> battleEffects)
        {
            _battleEffects = battleEffects;
        }
    }
}