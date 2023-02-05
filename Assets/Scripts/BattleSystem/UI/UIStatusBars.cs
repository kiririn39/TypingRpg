using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using Map;
using UnityEngine;

public class UIStatusBars : MonoBehaviour
{
    [SerializeField] private CharacterStatusBar playerStatusBar;
    [SerializeField] private CharacterStatusBar aiStatusBar;

    [SerializeField] private BattleCharacter playerCharacter;
    [SerializeField] private BattleCharacter aiCharacter;


    public void Awake()
    {
        playerCharacter.onInit += () =>
        {
            playerStatusBar.init(playerCharacter.HealthPoints, playerCharacter.MaxHealthPoints, playerCharacter.DelayNormalized);
            playerStatusBar.gameObject.SetActive(false);
        };
        aiCharacter    .onInit += () => playerStatusBar.init(aiCharacter.HealthPoints,     aiCharacter.MaxHealthPoints,     aiCharacter.DelayNormalized    );


        playerCharacter.onHealthChanged += (oldVal, newVal) => playerStatusBar.SetCurrentHealth(newVal);
        aiCharacter    .onHealthChanged += (oldVal, newVal) => aiStatusBar    .SetCurrentHealth(newVal);

        playerCharacter.onMaxHealthChanged += (oldVal, newVal) => playerStatusBar.SetMaxHealth(newVal);
        aiCharacter    .onMaxHealthChanged += (oldVal, newVal) => aiStatusBar    .SetMaxHealth(newVal);

        playerCharacter.onDelayNormalizedChanged += (oldVal, newVal) => playerStatusBar.SetCurrentDelayNormalized(newVal);
        aiCharacter    .onDelayNormalizedChanged += (oldVal, newVal) => aiStatusBar    .SetCurrentDelayNormalized(newVal);
        
        playerCharacter.onEffectsChanged += playerStatusBar.DisplayModificators;
        aiCharacter    .onEffectsChanged += aiStatusBar.DisplayModificators;

        StageManager.Instance.stageChangeStarted += resetEnemy;
        StageManager.Instance.gameReset          += resetEnemy;

        StageManager.Instance.stageChangeFinished += () => {
            if (StageManager.Instance.curStage.type == StageType.TOWN)
                return;

            playerStatusBar.gameObject.SetActive(true);
            aiStatusBar.gameObject.SetActive(true);
            playerCharacter.GetComponentInChildren<BattleCharacterAnimator>().play(BattleCharacterAnimator.AnimationType.IDLE);
        };

        StageManager.Instance.stageMovementStarted += tweenEnemy;
    }

    public void Update()
    {
        playerStatusBar.transform.position = Camera.main.WorldToScreenPoint(playerCharacter.trHealthBarPlace.position);
        aiStatusBar    .transform.position = Camera.main.WorldToScreenPoint(aiCharacter    .trHealthBarPlace.position);
    }

    public void tweenEnemy()
    {
        playerCharacter.GetComponentInChildren<BattleCharacterAnimator>().play(BattleCharacterAnimator.AnimationType.WALK);

        if (StageManager.Instance.curStage.type == StageType.TOWN)
            return;

        aiCharacter.gameObject.transform.DOLocalMoveX(2.33225012f, 2.0f).SetEase(Ease.InOutCubic);
    }

    private void teleportEnemy()
    {
        aiCharacter.gameObject.transform.position = new Vector3(15.0f, aiCharacter.gameObject.transform.position.y, aiCharacter.gameObject.transform.position.z);
    }

    private void resetEnemy()
    {
        if (StageManager.Instance.curStage?.type == StageType.TOWN)
            return;

        playerStatusBar.gameObject.SetActive(false);
        aiStatusBar.gameObject.SetActive(false);
        teleportEnemy();
    }
}
