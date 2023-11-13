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

        // ���ý����������ȣ�ȷ�������������֮��
        cornerCamera1.depth = mainCamera.depth + 1;
        cornerCamera2.depth = mainCamera.depth + 1;

        // ���ý����������Ұ���Σ������Ƿ�������Ļ�����½Ǻ����½�
        cornerCamera1.rect = new Rect(0, 0, 0.2f, 0.2f); // ���½�
        cornerCamera2.rect = new Rect(0.8f, 0, 0.2f, 0.2f); // ���½�
    }
}
