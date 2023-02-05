using System;
using System.Linq;
using BattleSystem.BattleActions;
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
            character.MaxHealthPoints = enemyConfig.healthPoints;
            character.HealthPoints = enemyConfig.healthPoints;

            character.ClearModificators();
            character.AddModificators(enemyConfig.modificators);

            possibleActions.Clear();
            possibleActions.AddRange(enemyConfig.possibleActions);

            aiIdleTime = enemyConfig.idleTime;

            character.SetCharacter(enemyConfig.character);
        }

        public override BattleActionBase GenerateNextAction()
        {
            if (lastTimeReturnedAnAction + aiIdleTime > Time.time)
                return chachedIdle;

            lastTimeReturnedAnAction = Time.time;

            var actions = possibleActions.Select(container => container.CloneAction());
            if (Math.Abs(character.HealthPoints - character.MaxHealthPoints) < 0.1f || character.actionModificators.OfType<HealTag>().Any())
                actions = actions.Where(action => action is not HealPrepareAction);
            if (character.actionModificators.OfType<PhysicalAttackDefence>().Any())
                actions = actions.Where(action => action is not DefencePrepareAction);
            if (character.actionModificators.OfType<PhysicalEvasion>().Any())
                actions = actions.Where(action => action is not EvasionPrepareAction);
            
            var action = actions.ToList().randomElement();

            action.Initialize(character);

            return action;
        }
    }
}