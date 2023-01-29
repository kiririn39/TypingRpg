using DefaultNamespace.BattleActions;

namespace DefaultNamespace
{
    public class EnemyCharacter : BattleCharacterBase
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