using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameBattleSystem : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter playerCharacter;
        [SerializeField] private EnemyCharacter enemyCharacter;

        private readonly List<BattleCharacterBase> _targets = new();


        private void Update()
        {
            ExecuteCharacterAction(playerCharacter);
            ExecuteCharacterAction(enemyCharacter);
        }

        private void ExecuteCharacterAction(BattleCharacterBase character)
        {
            _targets.Clear();
            var action = character.GetAction();

            if (action is ITargetsSelf)
                _targets.Add(action.GetCaster);

            if (action is ITargetsOpposingCharacter)
                _targets.Add(action.GetCaster == playerCharacter ? playerCharacter : enemyCharacter);

            action.ExecuteAction(_targets);
        }
    }
}