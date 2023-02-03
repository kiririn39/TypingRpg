using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFlyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;

    [SerializeField] private TMP_ColorGradient colorGradientHeal   = default;
    [SerializeField] private TMP_ColorGradient colorGradientDamage = default;
    [SerializeField] private TMP_ColorGradient colorGradientCrit   = default;


    public void init(float damageAmount, bool isHeal, bool isCrit = false)
    {
        TMP_ColorGradient colorGradient = colorGradientDamage;
        if (isHeal)
            colorGradient = colorGradientHeal;

        if (isCrit)
            colorGradient = colorGradientCrit;

        text.colorGradientPreset = colorGradient;

        text.text = ((int)damageAmount * 100).ToString();
    }
}
