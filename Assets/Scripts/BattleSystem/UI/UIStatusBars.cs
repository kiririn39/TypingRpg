using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatusBars : MonoBehaviour
{
    [SerializeField] private CharacterStatusBar playerStatusBar;
    [SerializeField] private CharacterStatusBar aiStatusBar;

    [SerializeField] private BattleCharacter playerCharacter;
    [SerializeField] private BattleCharacter aiCharacter;


    public void Awake()
    {
        playerCharacter.onInit += () => playerStatusBar.init(playerCharacter.HealthPoints, playerCharacter.MaxHealthPoints, playerCharacter.DelayNormalized);
        aiCharacter    .onInit += () => playerStatusBar.init(aiCharacter.HealthPoints,     aiCharacter.MaxHealthPoints,     aiCharacter.DelayNormalized    );


        playerCharacter.onHealthChanged += (oldVal, newVal) => playerStatusBar.SetCurrentHealth(newVal);
        aiCharacter    .onHealthChanged += (oldVal, newVal) => aiStatusBar    .SetCurrentHealth(newVal);

        playerCharacter.onMaxHealthChanged += (oldVal, newVal) => playerStatusBar.SetMaxHealth(newVal);
        aiCharacter    .onMaxHealthChanged += (oldVal, newVal) => aiStatusBar    .SetMaxHealth(newVal);

        playerCharacter.onDelayNormalizedChanged += (oldVal, newVal) => playerStatusBar.SetCurrentDelayNormalized(newVal);
        aiCharacter    .onDelayNormalizedChanged += (oldVal, newVal) => aiStatusBar    .SetCurrentDelayNormalized(newVal);
        
        playerCharacter.onEffectsChanged += playerStatusBar.DisplayModificators;
        aiCharacter    .onEffectsChanged += aiStatusBar.DisplayModificators;
    }

    public void Update()
    {
        playerStatusBar.transform.position = Camera.main.WorldToScreenPoint(playerCharacter.trHealthBarPlace.position);
        aiStatusBar    .transform.position = Camera.main.WorldToScreenPoint(aiCharacter    .trHealthBarPlace.position);
    }
}
