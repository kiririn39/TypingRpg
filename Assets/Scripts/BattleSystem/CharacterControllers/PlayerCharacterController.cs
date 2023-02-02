using System.Linq;
using DefaultNamespace.BattleActions;
using RuneStack;

namespace DefaultNamespace
{
    public class PlayerCharacterController : BattleCharacterControllerBase
    {
        private BattleActionBase nextBattleAction = null;


        public void init(UIRuneStack.ISkillTrigger skillTrigger)
        {
            skillTrigger.OnUseBattleAction += battleAction =>
            {
                battleAction.battleActionBase.Caster = character;
                this.nextBattleAction = battleAction.battleActionBase;
            };
        }

        public override BattleActionBase GenerateNextAction()
        {
            BattleActionBase action;

            if (nextBattleAction == null)
            {
                action = new IdleAction();
            }
            else
            {
                var nextActionType = nextBattleAction.GetType();

                action = possibleActions
                    .FirstOrDefault(actionContainer => actionContainer.CloneAction().GetType() == nextActionType)
                    .CloneAction();
            }

            action.Initialize(character);

            return action;
        }
    }
}