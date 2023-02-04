using System.Collections.Generic;
using BattleSystem.BattleActions.Containers;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyConfig : ScriptableObject
    {
        public float healthPoints;
        public float idleTime;
        public List<ActionContainerBase> possibleActions;
    }
}