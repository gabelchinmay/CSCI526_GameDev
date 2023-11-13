using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera cornerCamera1;
    public Camera cornerCamera2;

    void Start()
    {

        // 设置角落相机的深度，确保它们在主相机之上
        cornerCamera1.depth = mainCamera.depth + 1;
        cornerCamera2.depth = mainCamera.depth + 1;

        // 设置角落相机的视野矩形，将它们放置在屏幕的左下角和右下角
        cornerCamera1.rect = new Rect(0, 0, 0.2f, 0.2f); // 左下角
        cornerCamera2.rect = new Rect(0.8f, 0, 0.2f, 0.2f); // 右下角
    }
}
