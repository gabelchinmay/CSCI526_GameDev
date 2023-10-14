using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera localCamera;
    public Camera globalCamera;

    private bool isLocalView = true;

    void Start()
    {
        localCamera.enabled = true;
        globalCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            isLocalView = !isLocalView;
            localCamera.enabled = isLocalView;
            globalCamera.enabled = !isLocalView;
        }
    }
}
