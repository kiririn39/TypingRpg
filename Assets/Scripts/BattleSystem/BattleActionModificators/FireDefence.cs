using UnityEngine;

namespace BattleSystem.BattleActions
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionModificator/FireDefence")]
    public class FireDefence : ActionModificatorBase
    {
        public float damageMultiplier = 0.3f;
    }
}