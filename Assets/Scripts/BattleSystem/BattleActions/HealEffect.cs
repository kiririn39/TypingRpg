using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions
{
    [Serializable]
    public class HealEffect : BattleActionBase, IEffect, ITargetsSelf
    {
        public float HealPoints;
        public float HealsCount;
        public float DelayBetweenHeals;
        [HideInInspector] public float LastHealTimestamp;
        [SerializeField] private HealTag tag;

        private List<BattleActionBase> _battleEffects = null;


        public override void Initialize(BattleCharacter caster)
        {
            base.Initialize(caster);
            LastHealTimestamp = -DelayBetweenHeals;
        }

        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            if (!Caster.actionModificators.Contains(tag))
                Caster.AddModificator(tag);

            if (HealsCount <= 0)
                return FinishActionAndCleanUp(GameBattleSystem.FinishedAction);
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
                    SoundManager.Instance.playSound(SoundType.HEAL);
                }

            LastHealTimestamp = Time.time;
            HealsCount--;

            return HealsCount <= 0
                ? FinishActionAndCleanUp(GameBattleSystem.FinishedAction)
                : GameBattleSystem.InProgressAction;
        }

        private FinishedActionResult FinishActionAndCleanUp(FinishedActionResult result)
        {
            if (Caster.actionModificators.Contains(tag))
                Caster.RemoveModificator(tag);

            return result;
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
                InitializationTimestamp = this.InitializationTimestamp,
                tag = this.tag
            };

            return result;
        }

        public void SetAllEffectsLookup(List<BattleActionBase> battleEffects)
        {
            _battleEffects = battleEffects;
        }
    }
}