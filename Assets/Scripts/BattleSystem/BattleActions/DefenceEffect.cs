using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions
{
    [Serializable]
    public class DefenceEffect : BattleActionBase, IEffect, ITargetsSelf
    {
        public PhysicalAttackDefence Defence;
        public float DefenceDuration;

        private List<BattleActionBase> _battleEffects = null;


        public override void Initialize(BattleCharacter caster)
        {
            base.Initialize(caster);
        }

        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            var mostPowerfulDefence = _battleEffects.OfType<DefenceEffect>()
                .Where(action => action.Caster == this.Caster)
                .OrderBy(action => action.Defence.defencePercentage).FirstOrDefault();

            if (mostPowerfulDefence == this)
                foreach (var character in targets)
                {
                    if (!character.actionModificators.Contains(Defence))
                        character.AddModificator(Defence);

                    Debug.Log($"Caster {Caster.name} Protected themself");
                }

            if (DefenceDuration + InitializationTimestamp > Time.time)
                return GameBattleSystem.InProgressAction;

            foreach (var character in targets)
            {
                if (character.actionModificators.Contains(Defence))
                    character.RemoveModificator(Defence);

                Debug.Log($"Caster {Caster.name} Protect ended");
            }

            return GameBattleSystem.FinishedAction;
        }

        public override BattleActionBase Clone()
        {
            var result = new DefenceEffect
            {
                Defence = this.Defence,
                DefenceDuration = this.DefenceDuration,
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