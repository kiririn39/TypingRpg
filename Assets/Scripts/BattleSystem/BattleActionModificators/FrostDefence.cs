using UnityEngine;

namespace BattleSystem.BattleActions
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionModificator/FrostDefence")]
    public class FrostDefence : ActionModificatorBase
    {
        public float damageMultiplier = 0.3f;
    }
}