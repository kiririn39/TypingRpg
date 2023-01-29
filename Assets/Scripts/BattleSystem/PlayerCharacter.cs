using DefaultNamespace.BattleActions;

namespace DefaultNamespace
{
    public class PlayerCharacter : BattleCharacterBase
    {
        public override BattleActionBase GetAction()
        {
            return new IdleAction();
        }

        public override BattleActionBase GenerateNextAction()
        {
            return new IdleAction();
        }
    }
}