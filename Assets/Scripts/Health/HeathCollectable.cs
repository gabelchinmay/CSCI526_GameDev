using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathCollectable : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<Healthsystem>().AddHealth(healthValue);
                gameObject.SetActive(false);
            }
        }
    }
}
