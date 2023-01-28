using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BattleCharacterBase : MonoBehaviour
    {
        protected float Hitpoints;

        public abstract BattleActionBase GetNextAction();
    }
}