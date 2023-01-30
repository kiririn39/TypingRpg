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


        private void Update()
        {
            ExecuteCharacterAction(playerCharacter);
            ExecuteCharacterAction(enemyCharacter);
        }

        private void ExecuteCharacterAction(BattleCharacter character)
        {
            BattleActionBase action = character.GetAction();
            if (action == null)
                return;

            Debug.Log($"Character {character.name}, action {action}");

            _targets.Clear();
            if (action is ITargetsSelf)
                _targets.Add(action.Caster);

            if (action is ITargetsOpposingCharacter)
                _targets.Add(action.Caster == playerCharacter ? playerCharacter : enemyCharacter);

            if (!_targets.Any())
                return;

            bool hasFinished = action.ExecuteAction(_targets);
            if (hasFinished)
                character.GenerateNextAction();
        }
    }
}