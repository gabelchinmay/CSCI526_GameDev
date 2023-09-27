using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthsystem : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            //Player hurts
        }
        else
        {
            //Player died
            //Disable player movement when currentHealth == 0 (Testing only)
            GetComponent<PlayerController>().enabled = false;
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void Update()
    {
        //For Debugging Health System
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
}
