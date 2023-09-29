using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public string platformType;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        if( platformType == "Tri")
        {
            StartCoroutine(ColorTriChangingLoop());
        }
        else if(platformType == "Frost")
        {
            spriteRenderer.color = Color.cyan;
        }
        else if(platformType == "Lava")
        {
            spriteRenderer.color = Color.red;
        }
        else if(platformType == "Bi") {
            StartCoroutine(ColorBiChangingLoop());
        }
    }

    private void Update()
    {

    }

    private IEnumerator ColorTriChangingLoop()
    {
        while (true)
        {
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(5f);

            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(5f);

            spriteRenderer.color = Color.cyan;
            yield return new WaitForSeconds(5f);

        }
    }

    private IEnumerator ColorBiChangingLoop()
    {
        while (true)
        {


            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(5f);

            spriteRenderer.color = Color.cyan;
            yield return new WaitForSeconds(5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnPlatformEnter(this);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnPlatformExit(this);
            }
        }
    }


    public Color getSpriteColor()
    {
        return this.spriteRenderer.color;

    }

}
