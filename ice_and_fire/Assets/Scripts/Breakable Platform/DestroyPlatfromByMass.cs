using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyPlatfromByMass : MonoBehaviour
{
    private bool isFading = false;
    private float massThreshold = 5f;
    public float fadeSpeed = 1.0f;
    private SpriteRenderer spriteRenderer;
    private Color currentColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (isFading)
        { 
            currentColor.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = currentColor;

            if (currentColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().mass >= massThreshold)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.playerStyle == "fire")
            {
                isFading = true;
            }
        }
    }
}
