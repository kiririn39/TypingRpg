using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleActions.Containers;
using UnityEngine;

namespace DefaultNamespace
{
    public class BattleCharacterController : MonoBehaviour
    {
        [SerializeField] private BattleCharacter character;
        [SerializeField] [SerializeReference] private List<ActionContainerBase> possibleActions;


        public BattleActionBase GenerateNextAction()
        {
            var action = possibleActions.Select(container => container.CloneAction()).OrderBy(_ => Random.Range(-1, 1))
                .First();

            action.Initialize(character);

            return action;
        }
    }
}