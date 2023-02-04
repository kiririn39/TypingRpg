using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider bar           = null;
        [SerializeField] private Image  barFill       = null;
        [SerializeField] private Slider barBackground = null;
        [SerializeField] private float  tweenTime     = 0.1f;
        [SerializeField] private float  flashTime     = 0.1f;

        [SerializeField] private Material flashMaterial = null;

        private Sequence tweenCoroutine = null;
        private Sequence flashCoroutine = null;
        private Sequence flashColorCoroutine = null;


        public void init(float curValue, float maxValue)
        {
            bar.maxValue  = maxValue;
            bar.value     = curValue;

            barBackground.maxValue = maxValue;
            barBackground.value    = curValue;
        }

        public void initNormalized(float value)
        {
            SetValueNormalized(value);
        }

        public void SetMaxValue(float value)
        {
            bar.maxValue = value;
            barBackground.maxValue = value;
        }

        public void SetValue(float value)
        {
            bar.value = value;
            tween(value);
        }

        public void SetValueNormalized(float value)
        {
            bar.normalizedValue = value;
            tween(bar.maxValue * value);
        }

        private void tween(float value)
        {
            DOTween.Kill(tweenCoroutine);

            tweenCoroutine = DOTween.Sequence();
            tweenCoroutine.AppendInterval(tweenTime);
            tweenCoroutine.Append(barBackground.DOValue(value, tweenTime));
            tweenCoroutine.Play();
            
    
            flashCoroutine?.Complete();

            Material oldMaterial = barFill.material;
            barFill.material = flashMaterial;
            flashCoroutine = DOTween.Sequence();
            flashCoroutine.AppendInterval(flashTime);
            flashCoroutine.onComplete += () => barFill.material = oldMaterial;
            flashCoroutine.Play();


            flashColorCoroutine?.Complete();

            Color oldColor = barFill.color;
            flashColorCoroutine = DOTween.Sequence();
            flashColorCoroutine.Append(barFill.DOColor(Color.white, flashTime / 2));
            flashColorCoroutine.Append(barFill.DOColor(oldColor, flashTime / 2));
            flashColorCoroutine.onComplete += () => barFill.color = oldColor;
            flashColorCoroutine.Play();
        }
    }
}