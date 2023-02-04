using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions
{
    [Serializable]
    public class EvasionEffect : BattleActionBase, IEffect, ITargetsSelf
    {
        public PhysicalEvasion Evasion;
        public float EvasionDuration;

        private List<BattleActionBase> _battleEffects = null;


        public override void Initialize(BattleCharacter caster)
        {
            base.Initialize(caster);
        }

        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            var mostPowerfulEvasion = _battleEffects.OfType<EvasionEffect>()
                .Where(action => action.Caster == this.Caster)
                .OrderBy(action => action.Evasion.EvasionRate).FirstOrDefault();

            if (mostPowerfulEvasion == this)
                foreach (var character in targets)
                {
                    if (!character.actionModificators.Contains(Evasion))
                        character.AddModificator(Evasion);

                    Debug.Log($"Caster {Caster.name} is evading");
                }

            if (EvasionDuration + InitializationTimestamp > Time.time)
                return GameBattleSystem.InProgressAction;

            foreach (var character in targets)
            {
                if (character.actionModificators.Contains(Evasion))
                    character.RemoveModificator(Evasion);

                Debug.Log($"Caster {Caster.name} stopped evasion");
            }

            return GameBattleSystem.FinishedAction;
        }

        public override BattleActionBase Clone()
        {
            var result = new EvasionEffect()
            {
                Evasion = this.Evasion,
                EvasionDuration = this.EvasionDuration,
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