using Common;
using System;
using UnityEngine;
using System.Collections;


public class ParalaxObjectsController : MonoBehaviour
{
  #region Serialize Fields
  [SerializeField] private Vector2 maxShift    = default;
  [SerializeField] private Vector2 sensitivity = default;
  #endregion

  #region Private Fields
  private ParalaxObject[] _shiftingObjects = null;
  #endregion

  private void Awake()
  {
    init();
  }
  #region Public Methods
  public void init()
  {
    if (_shiftingObjects != null)
      return;

    _shiftingObjects = GetComponentsInChildren<ParalaxObject>();
    _shiftingObjects.forEach( it => it.init() );
  }
  #endregion

  public void move(float delta)
  {
    foreach ( ParalaxObject shiftingObject in _shiftingObjects )
      shiftingObject.move( delta, sensitivity, maxShift );
  }
}
