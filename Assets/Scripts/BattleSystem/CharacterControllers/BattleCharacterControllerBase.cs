using System.Collections.Generic;
using BattleSystem.BattleActions.Containers;
using UnityEngine;

namespace DefaultNamespace
{
    public class BattleCharacterControllerBase : MonoBehaviour
    {
        [SerializeField] protected BattleCharacter character;
        [SerializeField] [SerializeReference] protected List<ActionContainerBase> possibleActions;


        public virtual BattleActionBase GenerateNextAction()
        {
            return null;
        }
    }
}