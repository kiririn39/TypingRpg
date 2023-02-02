using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    public class CharacterStatusBar : MonoBehaviour
    {
        [SerializeField] private ProgressBar HealthBar;
        [SerializeField] private ProgressBar ActionDelayBar;


        public void SetMaxHealth(float value)
        {
            HealthBar.MaxValue = value;
        }

        public void SetCurrentHealth(float value)
        {
            HealthBar.SetValue(value);
        }

        public void SetMaxDelay(float value)
        {
            ActionDelayBar.MaxValue = value;
        }

        public void SetCurrentDelay(float value)
        {
            ActionDelayBar.SetValue(value);
        }

        public void SetCurrentHealthNormalized(float value) => HealthBar.SetValueNormalized(value);

        public void SetCurrentDelayNormalized(float value) => ActionDelayBar.SetValueNormalized(value);
    }
}