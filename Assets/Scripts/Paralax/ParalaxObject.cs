using Common;
using DG.Tweening;
using UnityEngine;


public class ParalaxObject : MonoBehaviour
{
  #region Serialize Fields
  [Header( "Settings" )]
  [SerializeField] private float farAway = 1.0f;
  #endregion

  #region Private Fields
  private const float SPEED_MULTIPLIER = 20.0f;
  private Vector2     _defaultPos      = default;
  #endregion


  #region Public Methods

  public void init()
  {
    if (_defaultPos == default)
      _defaultPos = transform.localPosition;
  }

  public void move( float delta, Vector2 sensitivityRaw, Vector2 bordersRaw )
  {
    Vector2 borders = bordersRaw / farAway;
    Vector2 sensitivity = sensitivityRaw / farAway;


    float newX = calcNewCoordinate(_defaultPos.x, transform.localPosition.x, borders.x, sensitivity.x * delta );
    transform.DOLocalMove(transform.localPosition.setX(newX), 2.0f).SetEase(Ease.InOutCubic);

    float calcNewCoordinate(float defaultPos, float curPos, float maxShifting, float rawDelta)
    {
      float farFromBorder = 1.0f - Mathf.InverseLerp(0.0f, maxShifting, Mathf.Abs(defaultPos - curPos));
      return Mathf.Clamp(curPos + (rawDelta / (farFromBorder).withMin(1.0f)) * Time.unscaledDeltaTime * SPEED_MULTIPLIER, defaultPos - maxShifting, defaultPos + maxShifting);
    }
  }
  #endregion
}