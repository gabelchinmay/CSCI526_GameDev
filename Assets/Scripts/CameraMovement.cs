using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera mainCamera;
    private float movingSpeed = 1.0f;
    private float elapsedTime = 0f;
    public float gameDuration = 30f;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < gameDuration)
        {
            mainCamera.transform.Translate(Vector3.down * Time.deltaTime * movingSpeed);
        }
    }
}
