using UnityEngine;

namespace BattleSystem.BattleActions
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionModificator/PhysicalAttackDefence")]
    public class PhysicalAttackDefence : ActionModificatorBase
    {
        public float defencePercentage = 0.9f;
    }
}