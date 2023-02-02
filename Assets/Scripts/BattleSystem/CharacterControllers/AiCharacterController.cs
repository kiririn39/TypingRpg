using System.Linq;
using Common;

namespace DefaultNamespace
{
    public class AiCharacterController : BattleCharacterControllerBase
    {
        public override BattleActionBase GenerateNextAction()
        {
            var action = possibleActions.Select(container => container.CloneAction()).ToList().randomElement();

            action.Initialize(character);

            return action;
        }
    }
}