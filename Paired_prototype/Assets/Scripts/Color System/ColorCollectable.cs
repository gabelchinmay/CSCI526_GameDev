using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCollectable : MonoBehaviour
{
    //Private variables
    private SpriteRenderer collectable_sr;
    private Color collectableColor;
    
    void Start()
    {
        collectable_sr = GetComponent<SpriteRenderer>();
        collectableColor = collectable_sr.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<ColorSystem>().ChangeColor(collectableColor);
            Destroy(this.gameObject);
        }
    }
}
