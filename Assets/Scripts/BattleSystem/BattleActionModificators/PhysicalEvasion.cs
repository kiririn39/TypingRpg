using UnityEngine;

namespace BattleSystem.BattleActions
{
    [CreateAssetMenu(menuName = "BattleSystem/ActionModificator/PhysicalEvasion")]
    public class PhysicalEvasion : ActionModificatorBase
    {
        public float EvasionRate = 0.5f;
    }
}