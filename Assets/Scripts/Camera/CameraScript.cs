using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Camera camera;


    public void tweenDamageTaken()
    {
        camera.DOShakeRotation(1.0f, new Vector3(0, 0, 1));
    }
}
