using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleActions;
using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    public class CharacterStatusBar : MonoBehaviour
    {
        [SerializeField] private ProgressBar HealthBar;
        [SerializeField] private ProgressBar ActionDelayBar;

        // modificator images
        [SerializeField] private GameObject defenceEffect;
        [SerializeField] private GameObject evasionEffect;
        [SerializeField] private GameObject healEffect;
        [SerializeField] private GameObject poisonEffect;
        [SerializeField] private GameObject frostDefenceEffect;
        [SerializeField] private GameObject fireDefenceEffect;


        public void init(float curHealth, float maxHealth, float delayNormalized)
        {
            HealthBar.init(curHealth, maxHealth);
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

        public void DisplayModificators(List<ActionModificatorBase> modificators)
        {
            defenceEffect.SetActive(modificators.OfType<PhysicalAttackDefence>().Any());
            evasionEffect.SetActive(modificators.OfType<PhysicalEvasion>().Any());
            healEffect.SetActive(modificators.OfType<HealTag>().Any());
            poisonEffect.SetActive(modificators.OfType<PoisonTag>().Any());
            frostDefenceEffect.SetActive(modificators.OfType<FrostDefence>().Any());
            fireDefenceEffect.SetActive(modificators.OfType<FireDefence>().Any());
        }
    }
}