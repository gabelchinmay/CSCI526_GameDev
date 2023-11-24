using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
    // interlaced change for bi-color platforms
    public bool anotherOrder = false;
    public float biFrequencyTime = 10.0f;
    public float triFrequencyTime = 5.0f;
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
        else if(platformType == "lavaBi") 
        {
            StartCoroutine(lavaBiChangingLoop());
        }
        else if(platformType == "frostBi") 
        {
            StartCoroutine(frostBiChangingLoop());
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
            yield return new WaitForSeconds(triFrequencyTime);

            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(triFrequencyTime);

            spriteRenderer.color = Color.cyan;
            yield return new WaitForSeconds(triFrequencyTime);

        }
    }

    private IEnumerator ColorBiChangingLoop()
    {
        // if anotherOrder is true, change from frost
        if (anotherOrder){
            while (true)
            {
                spriteRenderer.color = Color.cyan;
                yield return new WaitForSeconds(biFrequencyTime);

                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(biFrequencyTime);
            }
        }
        else{
            while (true)
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(biFrequencyTime);

                spriteRenderer.color = Color.cyan;
                yield return new WaitForSeconds(biFrequencyTime);
            }
        }
        
    }

    // change between lava and normal
    private IEnumerator lavaBiChangingLoop()
    {
        // if anotherOrder is true, change from normal
        if (anotherOrder){
            while (true)
            {
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(biFrequencyTime);

                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(biFrequencyTime);
            }
        }
        else{
            while (true)
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(biFrequencyTime);

                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(biFrequencyTime);
            }
        }
        
    }

    // change between frost and normal
    private IEnumerator frostBiChangingLoop()
    {
        // if anotherOrder is true, change from normal
        if (anotherOrder){
            while (true)
            {
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(biFrequencyTime);

                spriteRenderer.color = Color.cyan;
                yield return new WaitForSeconds(biFrequencyTime);
            }
        }
        else{
            while (true)
            {
                spriteRenderer.color = Color.cyan;
                yield return new WaitForSeconds(biFrequencyTime);

                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(biFrequencyTime);
            }
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
                    player.GetComponent<PlayerController>().TakeDamage(5);
                    if (sendToGoogle != null)
                    {
                        sendToGoogle.firePlatDamage();

                    }

                    //speed = 10.0f;
                }

                else if (spriteRenderer.color == Color.cyan && player.GetComponent<PlayerController>().getPlayerStyle() != "ice")
                {
                    player.GetComponent<PlayerController>().TakeDamage(2);
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
