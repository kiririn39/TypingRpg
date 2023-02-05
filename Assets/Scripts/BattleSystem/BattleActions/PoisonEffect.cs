using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace BattleSystem.BattleActions
{
    [Serializable]
    public class PoisonEffect : BattleActionBase, IEffect, ITargetsOpposingCharacter
    {
        public float AttackPoints;
        public float AttacksCount;
        public float DelayBetweenAttacks;
        [HideInInspector] public float LastAttackTimestamp;
        [SerializeField] private PoisonTag tag;


        private List<BattleActionBase> _battleEffects = null;


        public override void Initialize(BattleCharacter caster)
        {
            base.Initialize(caster);
            LastAttackTimestamp = InitializationTimestamp;
        }

        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            if (AttacksCount <= 0)
                return FinishActionAndCleanUp(GameBattleSystem.FinishedAction, targets);
            if (LastAttackTimestamp + DelayBetweenAttacks > Time.time)
                return GameBattleSystem.InProgressAction;

            var mostPowerfulPoison = _battleEffects.OfType<PoisonEffect>()
                .Where(action => action.Caster == this.Caster)
                .OrderByDescending(action => action.AttackPoints).FirstOrDefault();

            if (mostPowerfulPoison == this)
                foreach (var character in targets)
                {
                    if (!character.actionModificators.Contains(tag))
                        character.AddModificator(tag);

                    character.DealDamage(AttackPoints, GetType());
                    Debug.Log($"Caster {Caster.name} Poisoning {character.name}");
                    SoundManager.Instance.playSound(SoundType.POISON);
                }

            LastAttackTimestamp = Time.time;
            AttacksCount--;

            return AttacksCount <= 0
                ? FinishActionAndCleanUp(GameBattleSystem.FinishedAction, targets)
                : GameBattleSystem.InProgressAction;
        }

        private FinishedActionResult FinishActionAndCleanUp(FinishedActionResult result, List<BattleCharacter> targets)
        {
            foreach (var character in targets)
                if (character.actionModificators.Contains(tag))
                    character.RemoveModificator(tag);

            return result;
        }

        public override BattleActionBase Clone()
        {
            var result = new PoisonEffect
            {
                AttackPoints = this.AttackPoints,
                AttacksCount = this.AttacksCount,
                DelayBetweenAttacks = this.DelayBetweenAttacks,
                LastAttackTimestamp = this.LastAttackTimestamp,
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