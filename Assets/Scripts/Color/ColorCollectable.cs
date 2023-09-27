using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCollectable : MonoBehaviour
{
    //Private variables
    private SpriteRenderer sr;
    private Color collectableColor;
    
    //Public variables
    public String colorTag;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collectableColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<ColorSystem>().ChangeColor(collectableColor);
            collcted();
        }
    }

    private void collcted()
    {
        sr.enabled = false;
    }
}
