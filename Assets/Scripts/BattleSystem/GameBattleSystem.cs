using UnityEngine;

namespace DefaultNamespace
{
    public class GameBattleSystem : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter playerCharacter;
        [SerializeField] private EnemyCharacter enemyCharacter;

        private int _turnCount = 0;
    }
}