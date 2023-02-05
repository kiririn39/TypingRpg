using System.Collections.Generic;
using BattleSystem.BattleActions;
using BattleSystem.BattleActions.Containers;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "BattleSystem/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public float healthPoints;
        public float idleTime;
        public List<ActionContainerBase> possibleActions;
        public List<ActionModificatorBase> modificators;
        public BattleCharacterAnimator.Character character;
    }
}