using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleActions.Containers;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace DefaultNamespace
{
    public class BattleCharacterController : MonoBehaviour
    {
        [SerializeField] private BattleCharacter character;
        [SerializeField] [SerializeReference] private List<ActionContainerBase> possibleActions;


        public BattleActionBase GenerateNextAction()
        {
            var action = possibleActions.Select(container => container.CloneAction()).OfType<AttackAction>().First();

            action.Initialize(character);

            return action;
        }
    }
}