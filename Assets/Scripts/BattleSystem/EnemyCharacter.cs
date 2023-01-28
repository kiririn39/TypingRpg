using DefaultNamespace.BattleActions;

namespace DefaultNamespace
{
    public class EnemyCharacter : BattleCharacterBase
    {
        public override BattleActionBase GetNextAction()
        {
            return new IdleAction();
        }
    }
}