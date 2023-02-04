using System;
using System.Linq;
using Common;
using DefaultNamespace.BattleActions;
using UnityEngine;

namespace DefaultNamespace
{
    public class AiCharacterController : BattleCharacterControllerBase
    {
        private float aiIdleTime = 0.0f;
        private float lastTimeReturnedAnAction = 0.0f;
        private IdleAction chachedIdle;


        private void Awake()
        {
            chachedIdle = new IdleAction();
            chachedIdle.Initialize(character);
        }

        public void InitializeEnemy(EnemyConfig enemyConfig)
        {
            character.HealthPoints = enemyConfig.healthPoints;
            character.MaxHealthPoints = enemyConfig.healthPoints;

            character.ClearModificators();
            character.AddModificators(enemyConfig.modificators);

            possibleActions.Clear();
            possibleActions.AddRange(enemyConfig.possibleActions);

            aiIdleTime = enemyConfig.idleTime;
        }

        public override BattleActionBase GenerateNextAction()
        {
            if (lastTimeReturnedAnAction + aiIdleTime > Time.time)
                return chachedIdle;

            lastTimeReturnedAnAction = Time.time;
            var action = possibleActions.Select(container => container.CloneAction()).ToList().randomElement();

            action.Initialize(character);

            return action;
        }
    }
}