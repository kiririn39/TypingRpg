using UnityEngine;

namespace Utilities
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer bar;
        [SerializeField] private float barMaxWidth;

        public float MaxValue;
        public float CurrentValue;

        public void SetValue(float value)
        {
            CurrentValue = value;
            CurrentValue = CurrentValue > 0 ? CurrentValue : 0;
            var widthFactor = CurrentValue / MaxValue;
            widthFactor = float.IsInfinity(widthFactor) ? 0 : widthFactor;

            var scale = bar.size;
            scale.x = widthFactor;

            bar.size = scale;
        }
    }
}