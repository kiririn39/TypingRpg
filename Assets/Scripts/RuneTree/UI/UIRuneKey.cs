using Assets.Scripts.SkillTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRuneKey : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtRuneKey = null;
    [SerializeField] private RuneKey runeKey = RuneKey.NONE;


    [Editor]
    private void OnValidate()
    {
        updateUI();
    }

    public void init(RuneKey runeKey)
    {
        RuneKey = runeKey;
    }

    public RuneKey RuneKey
    {
        get => runeKey;
        set
        {
            runeKey = value;
            updateUI();
        }
    }

    private void updateUI()
    {
        if (txtRuneKey == null)
            return;

        txtRuneKey.text = runeKey == RuneKey.NONE ? "?" :runeKey.ToString();
    }
}
