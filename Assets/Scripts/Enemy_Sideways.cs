using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Healthsystem>().TakeDamage(damage);
        }


    }
}
