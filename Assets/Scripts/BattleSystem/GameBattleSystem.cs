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
            BattleActionBase action = character.GetAction();
            if (action == null)
                return;

            _targets.Clear();
            if (action is ITargetsSelf)
                _targets.Add(action.Caster);

            if (action is ITargetsOpposingCharacter)
                _targets.Add(action.Caster == playerCharacter ? playerCharacter : enemyCharacter);

            if (_targets.Count > 0)
                action.ExecuteAction(_targets);
        }
    }
}