using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public string platformType;
    public GameObject player;
    public float rotationSpeed = 30f;
    private bool isOnPlatform = false;
    private SendToGoogle sendToGoogle;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        sendToGoogle = FindObjectOfType<SendToGoogle>();
        if ( platformType == "Tri")
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
        else if(platformType == "Bi") 
        {
            StartCoroutine(ColorBiChangingLoop());
        }
        else if(platformType == "Normal")
        {
            spriteRenderer.color = originalColor;
        }
        StartCoroutine(InflictDamagesFromPlatform());
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
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
            this.isOnPlatform = true;
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
            this.isOnPlatform = false;
        }
    }


    public Color getSpriteColor()
    {
        return this.spriteRenderer.color;

    }

    private IEnumerator InflictDamagesFromPlatform()
    {
        while (true)
        {
            //Debug.Log((spriteRenderer.color == Color.red).ToString() + this.playerStyle + isOnPlatform);



            if (this.isOnPlatform)
            {
                //currentColour = CurrPlatform.getSpriteColor();


                if (spriteRenderer.color == Color.red && player.GetComponent<PlayerController>().getPlayerStyle() != "fire")
                {
                    //this.TakeDamage(10);
                    player.GetComponent<PlayerController>().TakeDamage(10);
                    if (sendToGoogle != null)
                    {
                        sendToGoogle.firePlatDamage();

                    }

                    //speed = 10.0f;
                }

                else if (spriteRenderer.color == Color.cyan && player.GetComponent<PlayerController>().getPlayerStyle() != "ice")
                {
                    player.GetComponent<PlayerController>().TakeDamage(1);
                    if (sendToGoogle != null)
                    {
                        sendToGoogle.icePlatDamage();

                    }
                    //this.TakeDamage(1);
                    //speed = 1.0f;
                }
                else
                {
                    //speed = 10.0f;
                }
            }


            yield return new WaitForSeconds(1.0f);

        }
    }

}
