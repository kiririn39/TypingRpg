using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace BattleSystem.BattleActions
{
    [Serializable]
    public class MagicFireAction : BattleActionBase, ITargetsOpposingCharacter
    {
        public float AttackPoints;
        public float ExecutionDelay;
        public float DamageInflictedTime;

        private bool isDamageInflicted = false;

        public override void Initialize(BattleCharacter Caster)
        {
            base.Initialize(Caster);

            Caster.playAnimation(BattleCharacterAnimator.AnimationType.ATTACK_FIRE);
            isDamageInflicted = false;
        }

        protected override ActionResultBase ExecuteActionImpl(List<BattleCharacter> targets)
        {
            float completesAt = InitializationTimestamp + ExecutionDelay;
            float damageInflictedTime = InitializationTimestamp + DamageInflictedTime;

            Caster.DelayNormalized = (Time.time - InitializationTimestamp) / ExecutionDelay;

            if (damageInflictedTime < Time.time && !isDamageInflicted)
            {
                foreach (var battleCharacter in targets)
                {
                    var fireDefences = battleCharacter.actionModificators.OfType<FireDefence>();
                    var attackPoint = AttackPoints;

                    foreach (var attackDefence in fireDefences)
                        attackPoint *= attackDefence.damageMultiplier;

                    battleCharacter.DealDamage(attackPoint, GetType());
                    battleCharacter.playAnimation(BattleCharacterAnimator.AnimationType.TAKE_DAMAGE);
                    SoundManager.Instance.playSound(SoundType.FIREBALL);
                    SoundManager.Instance.shakeCamera();
                }

                isDamageInflicted = true;
            }

            if (completesAt > Time.time)
                return GameBattleSystem.InProgressAction;

            return GameBattleSystem.FinishedAction;
        }

        public override BattleActionBase Clone()
        {
            return new MagicFireAction
            {
                Caster = base.Caster,
                AttackPoints = this.AttackPoints,
                ExecutionDelay = this.ExecutionDelay,
                DamageInflictedTime = this.DamageInflictedTime,
            };
        }

        private void UpdateCharacterStatusBar(BattleCharacter character, float normalizedValue)
        {
        }
    }
}