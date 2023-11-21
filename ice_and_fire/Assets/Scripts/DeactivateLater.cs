using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateLater : MonoBehaviour
{
    private float timeUp = 20.0f;
    void Start()
    {
        StartCoroutine(StartCountDown(timeUp));
    }

    private IEnumerator StartCountDown(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}
