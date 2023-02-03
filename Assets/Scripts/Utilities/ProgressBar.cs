using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider bar = null;

        public void init(float curValue, float maxValue)
        {
            bar.maxValue  = maxValue;
            bar.value     = curValue;
        }

        public void initNormalized(float value)
        {
            bar.normalizedValue = value;
        }

        public void SetMaxValue(float value)
        {
            bar.maxValue = value;
        }

        public void SetValue(float value)
        {
            bar.value = value;
        }

        public void SetValueNormalized(float value)
        {
            bar.normalizedValue = value;
        }
    }
}