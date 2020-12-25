using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private float defaultWidth;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        defaultWidth = mainCamera.orthographicSize * mainCamera.aspect;
    }
    private void Update()
    {
        mainCamera.orthographicSize = defaultWidth / mainCamera.aspect;
    }
}
