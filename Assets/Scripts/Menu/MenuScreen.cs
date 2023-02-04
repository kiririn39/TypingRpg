using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MenuScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform menuRectTransform;
        [SerializeField] private CanvasGroup   canvasGroup;
        [SerializeField] private Slider        musicVolumeSlider;
        [SerializeField] private Slider        sfxVolumeSlider;
        [SerializeField] private RectTransform gameNameRectTransform;
        [SerializeField] private RectTransform menuButtonsRectTransform;
        [SerializeField] private TMP_Text      gameNameText;
        [SerializeField] private CanvasGroup   buttonsCanvasGroup;
        [SerializeField] private RectTransform screenControls;
        [SerializeField] private RectTransform screenCredits;
        [SerializeField] private RectTransform screenSettings;

        private StageManager stageManager;

        private void Awake()
        {
            canvasGroup.alpha = 1;
            tweenMenuStart();
        }

        private void Start()
        {
            SoundManager.Instance.playMenuMusic();
            musicVolumeSlider.onValueChanged.AddListener(SoundManager.Instance.setMusicVolume);
            sfxVolumeSlider.onValueChanged.AddListener(SoundManager.Instance.setSFXVolume);
        }


        public void startGame()
        {
            SoundManager.Instance.fadeToGameMusic();
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
            menuRectTransform.anchoredPosition = new Vector2(2560, menuRectTransform.anchoredPosition.y);
            menuRectTransform.DOAnchorPosX(0, 2.0f).SetEase(Ease.InOutQuad);
        }

        public void showCredits()
        {
            menuRectTransform.DOAnchorPosX(-2560.0f, 2.0f).SetEase(Ease.InOutQuad);
            screenCredits.DOAnchorPosX(0, 2.0f).SetEase(Ease.InOutQuad);
        }

        public void showControls()
        {
            menuRectTransform.DOAnchorPosX(-2560.0f, 2.0f).SetEase(Ease.InOutQuad);
            screenControls.DOAnchorPosX(0, 2.0f).SetEase(Ease.InOutQuad);
        }

        public void showSettings()
        {
            menuRectTransform.DOAnchorPosX(-2560.0f, 2.0f).SetEase(Ease.InOutQuad);
            screenSettings.DOAnchorPosX(0, 2.0f).SetEase(Ease.InOutQuad);
        }

        public void backToMenuFromCredits()
        {
            menuRectTransform.DOAnchorPosX(0.0f, 2.0f).SetEase(Ease.InOutQuad);
            screenCredits.DOAnchorPosX(2560.0f, 2.0f).SetEase(Ease.InOutQuad);
        }

        public void backToMenuFromControls()
        {
            menuRectTransform.DOAnchorPosX(0.0f, 2.0f).SetEase(Ease.InOutQuad);
            screenControls.DOAnchorPosX(2560.0f, 2.0f).SetEase(Ease.InOutQuad);
        }

        public void backToMenuFromSettings()
        {
            menuRectTransform.DOAnchorPosX(0.0f, 2.0f).SetEase(Ease.InOutQuad);
            screenSettings.DOAnchorPosX(2560.0f, 2.0f).SetEase(Ease.InOutQuad);
        }

        private void tweenMenuStart()
        {
            Vector2 game_name_anchored_pos = gameNameRectTransform.anchoredPosition;
            Vector2 buttons_anchored_pos   = menuButtonsRectTransform.anchoredPosition;
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(
                () => {
                    gameNameRectTransform.anchoredPosition += Vector2.left * 100;
                    menuButtonsRectTransform.anchoredPosition += Vector2.down * 100;

                    gameNameText.alpha = 0.0f;
                    buttonsCanvasGroup.alpha = 0.0f;
                    buttonsCanvasGroup.interactable = false;
                })
                .Append(gameNameText.DOFade(1.0f, 1.0f).SetEase(Ease.OutQuad))
                .Join(gameNameRectTransform.DOAnchorPos(game_name_anchored_pos, 1.0f).SetEase(Ease.OutQuad))
                .Append(menuButtonsRectTransform.DOAnchorPos(buttons_anchored_pos, 1.0f).SetEase(Ease.OutQuad))
                .Join(buttonsCanvasGroup.DOFade(1.0f, 1.0f).SetEase(Ease.OutQuad))
                .AppendCallback(
                    () => {
                        buttonsCanvasGroup.interactable = true;
                    })
                .Play();
        }
    }
}
