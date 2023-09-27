using System;
using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Healthsystem playerHealth;
    [SerializeField] private UnityEngine.UI.Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
