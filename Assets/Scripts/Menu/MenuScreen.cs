﻿using DG.Tweening;
using Managers;
using UnityEngine;

namespace Menu
{
    public class MenuScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup   canvasGroup;

        private StageManager stageManager;


        public void startGame()
        {
            canvasGroup.blocksRaycasts = false;
            // ui_panel_runes_go ??= FindObjectOfType<UIPanelRunes>(true).gameObject;
            // ui_panel_runes_go.SetActive(true);
            stageManager ??= FindObjectOfType<StageManager>();
            stageManager.prepareGame();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(canvasGroup.DOFade(0.0f, 2.0f).SetEase(Ease.InQuart));
            sequence.AppendCallback((() => {
                stageManager.startGame();
            }));
        }

        public void showAfterLost()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1.0f;
            rectTransform.anchoredPosition = new Vector2(2560, rectTransform.anchoredPosition.y);
            rectTransform.DOAnchorPosX(0, 2.0f).SetEase(Ease.InOutQuad);
        }
    }
}