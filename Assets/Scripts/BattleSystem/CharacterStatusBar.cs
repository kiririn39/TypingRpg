using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    public class CharacterStatusBar : MonoBehaviour
    {
        [SerializeField] private ProgressBar HealthBar;
        [SerializeField] private ProgressBar ActionDelayBar;

        public void init(float curHealth, float maxHealth, float delayNormalized)
        {
            HealthBar     .init(curHealth, maxHealth);
            ActionDelayBar.initNormalized(delayNormalized);
        }

        public void SetMaxHealth(float value)
        {
            HealthBar.SetMaxValue(value);
        }

        public void SetCurrentHealth(float value)
        {
            HealthBar.SetValue(value);
        }

        public void SetCurrentHealthNormalized(float value) => HealthBar.SetValueNormalized(value);

        public void SetCurrentDelayNormalized(float value) => ActionDelayBar.SetValueNormalized(value);
    }
}