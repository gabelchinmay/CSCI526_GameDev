using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSystem : MonoBehaviour
{
    //Private variables
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = Color.white;
    }

    public void ChangeColor(Color collectableColor)
    {
        sr.color = collectableColor;
    }
}
