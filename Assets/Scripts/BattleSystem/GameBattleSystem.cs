using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameBattleSystem : MonoBehaviour
    {
        [SerializeField] private BattleCharacter playerCharacter;
        [SerializeField] private BattleCharacter enemyCharacter;

        private readonly List<BattleCharacter> _targets = new();
        private readonly List<BattleActionBase> _passiveActions = new();

        public static readonly FinishedActionResult FinishedAction = new FinishedActionResult();
        public static readonly InProcessActionResult InProgressAction = new InProcessActionResult();


        private void Update()
        {
            BattleActionBase action = playerCharacter.GetAction();
            if (action is not IEffect)
                ExecuteAction(playerCharacter, action);
            else
            {
                _passiveActions.Add(action);
                playerCharacter.GenerateNextAction();
            }

            action = enemyCharacter.GetAction();
            if (action is not IEffect)
                ExecuteAction(enemyCharacter, action);
            else
            {
                _passiveActions.Add(action);
                enemyCharacter.GenerateNextAction();
            }

            ExecutePassives();
        }

        private void ExecuteAction(BattleCharacter character, BattleActionBase action)
        {
            if (action == null)
                return;

            _targets.Clear();
            if (action is ITargetsSelf)
                _targets.Add(action.Caster);

            if (action is ITargetsOpposingCharacter)
                _targets.Add(action.Caster == playerCharacter ? enemyCharacter : playerCharacter);

            if (!_targets.Any())
                return;

            var actionState = action.ExecuteAction(_targets);

            if (actionState is FinishedActionResult)
            {
                if (actionState is FinishedWithEffectActionResult actionWithEffect)
                    _passiveActions.Add(actionWithEffect.effect.Clone());

                if (action is IEffect)
                    _passiveActions.Remove(action);
                else
                    character.GenerateNextAction();
            }
        }

        private void ExecutePassives()
        {
            foreach (var passiveAction in _passiveActions)
            {
                var effect = passiveAction as IEffect;
                effect.SetAllEffectsLookup(_passiveActions);
                ExecuteAction(passiveAction.Caster, passiveAction);
            }
        }
    }

    public class ActionResultBase
    {
    }

    public class FinishedActionResult : ActionResultBase
    {
    }

    public class InProcessActionResult : ActionResultBase
    {
    }

    public class FinishedWithEffectActionResult : FinishedActionResult
    {
        public BattleActionBase effect;
    }
}