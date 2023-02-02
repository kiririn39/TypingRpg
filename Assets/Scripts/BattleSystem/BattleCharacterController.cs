using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleActions.Containers;
using Common;
using Managers;
using RuneStack;
using UnityEngine;

namespace DefaultNamespace
{
    public class BattleCharacterController : MonoBehaviour
    {
        [SerializeField] private BattleCharacter character;
        [SerializeField] [SerializeReference] private List<ActionContainerBase> possibleActions;

        private BattleActionBase nextBattleAction = null;

        public void init(UIRuneStack.ISkillTrigger skillTrigger )
        {
            skillTrigger.OnUseBattleAction += battleAction =>
            {
                battleAction.battleActionBase.Caster = character;
                this.nextBattleAction = battleAction.battleActionBase;
            };
        }

        public BattleActionBase GenerateNextAction()
        {
            var battleAction = getBattleAction();
            battleAction.Caster = character;

            return battleAction;

            var action = possibleActions.Select(container => container.CloneAction()).ToList().randomElement();

            action.Initialize(character);

            return action;
        }

        private BattleActionBase getBattleAction()
        {
            if (possibleActions.All(it => it.CloneAction().GetType() != nextBattleAction?.GetType()))
                return PlayerDatabase.idleSkill.RuneBattleActionInfo.battleActionBase;

            if (nextBattleAction != null)
                return nextBattleAction;

            return PlayerDatabase.idleSkill.RuneBattleActionInfo.battleActionBase;
        }
    }
}