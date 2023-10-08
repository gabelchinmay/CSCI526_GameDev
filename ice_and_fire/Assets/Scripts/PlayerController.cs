using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 10.0f;
    private float jumpForce = 7.0f;
    private int jumpCount;
    private int maxJumps = 2;
    private bool isGameOver = false;
    private bool canJump = true;
    private bool isOnPlatform = false;
    private Color currentColour;
    private PlatformController CurrPlatform = null;
    public int maxHealth = 100;
    private int currentHealth;
    public TMP_Text healthText;
    public TMP_Text gameOverText;
    public TMP_Text InventoryText;
    public Image HealthSkeleton;
    public Image HealthBar;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    private Vector3 playerPreviousPosition;
    public GameObject placeholderPrefab;
    private bool isOnDefrost = false;
    private KeyGateController key = null;

    // new Feature: Invicible Shield, boolean variable to show if this player is shielded or not
    private bool isShielded = false;
    private bool isOnSpike = false;
    private bool isOnMonster = false;
    private bool isOnSaw = false;

    public float playerMassMultiplicationFactor = 100f;
    public float playerJumpForceMultiplicationFactor = 100f;

    public GameOverScreen gameOverScreen;

    void Start()
    {

        currentHealth = maxHealth;
        UpdateHealthUI();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(InflictDamages());

    }

    void Update()
    {
        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        rb = GetComponent<Rigidbody2D>();
        currentColour = Color.black;

        if (CurrPlatform != null)
        {
            currentColour = CurrPlatform.getSpriteColor();
        }
        // On Lava/Frost
        if (!isOnPlatform)
        {
            //Debug.Log("On platform");
            speed = 10.0f;
            canJump = true;

        }
        else
        {
            if (currentColour == Color.red || (currentColour == Color.cyan && !isOnDefrost))
            {
                canJump = false;
            }
            else
            {
                canJump = true;
            }
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps && canJump)
        {
            Jump();
            jumpCount++;
            sendToGoogle.addJump();
            if (jumpCount == maxJumps)
            {
                canJump = false;
                //StartCoroutine(ResetJumpCooldown());
            }
        }

        // Movements
        if (!isGameOver)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);


        }
        else
        {
            rb.velocity = Vector2.zero;
        }



        if (inventory.Count != 0)
        {
            InventoryText.text = "";
            InventoryText.text += "INVENTORY";

            if (inventory.ContainsKey("HealthUp"))
            {
                InventoryText.text += "\nHealthUp: " + inventory["HealthUp"];

            }
            if (inventory.ContainsKey("JumpHigher"))
            {
                InventoryText.text += "\nJumpHighers: " + inventory["JumpHigher"];

            }

            if (inventory.ContainsKey("Defrost"))
            {
                InventoryText.text += "\nDefrosts: " + inventory["Defrost"];
            }
            if (inventory.ContainsKey("Placeholder"))
            {
                InventoryText.text += "\nPlaceholders: " + inventory["Placeholder"];
            }

            if (inventory.ContainsKey("InvincibleShield")) // Inverntory text for invincible shield
            {
                InventoryText.text += "\nInvincibleShield: " + inventory["InvincibleShield"];
            }
            if (inventory.ContainsKey("Key"))
            {
                InventoryText.text += "\nKeys: " + inventory["Key"];
            }
        }
        else
        {
            InventoryText.text = "";

        }

        if (Input.GetKeyDown(KeyCode.K) && inventory.ContainsKey("Placeholder"))
        {

            playerPreviousPosition = transform.position;
            Vector3 placeholderPosition = playerPreviousPosition - Vector3.up * 0.7f;
            if (rb.mass < 9)
            {
                //Debug.Log("Old mass! " + rb.mass);
                transform.Translate(Vector2.right * 1.1f);
            }
            else
            {
                //Debug.Log("Increased mass! " + rb.mass);
                transform.Translate(Vector2.right * 1.3f);
            }

            Instantiate(placeholderPrefab, placeholderPosition, Quaternion.identity);

            if (inventory.ContainsKey("Placeholder"))
            {
                inventory["Placeholder"]--;
                if (inventory["Placeholder"] <= 0)
                {
                    inventory.Remove("Placeholder");
                }
            }

        }


        if (Input.GetKeyDown(KeyCode.J) && inventory.ContainsKey("JumpHigher"))
        {
            StartCoroutine(JumpHigherPowerUp());

            if (inventory.ContainsKey("JumpHigher"))
            {
                inventory["JumpHigher"]--;
                if (inventory["JumpHigher"] <= 0)
                {
                    inventory.Remove("JumpHigher");
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.H) && inventory.ContainsKey("HealthUp"))
        {
            if (currentHealth <= maxHealth - 10)
            {
                currentHealth += 10;

            }
            else
            {
                currentHealth = maxHealth;
            }
            UpdateHealthUI();

            if (inventory.ContainsKey("HealthUp"))
            {
                inventory["HealthUp"]--;
                if (inventory["HealthUp"] <= 0)
                {
                    inventory.Remove("HealthUp");
                }
            }

        }


        if (Input.GetKeyDown(KeyCode.F) && inventory.ContainsKey("Defrost"))
        {
            StartCoroutine(DefrostPowerUp());

            if (inventory.ContainsKey("Defrost"))
            {
                inventory["Defrost"]--;
                if (inventory["Defrost"] <= 0)
                {
                    inventory.Remove("Defrost");
                }
            }

        }

        // Update for invincible shields
        if (Input.GetKeyDown(KeyCode.Q) && inventory.ContainsKey("InvincibleShield"))
        {
            // Press Q to use this item
            StartCoroutine(ActivateShield());

            if (inventory.ContainsKey("InvincibleShield"))
            {
                inventory["InvincibleShield"]--;
                if (inventory["InvincibleShield"] <= 0)
                {
                    inventory.Remove("InvincibleShield");
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.O) && inventory.ContainsKey("Key"))
        {
            //StartCoroutine(JumpHigherPowerUp());
            if (key != null)
            {
                key.openGate();
            }

            if (inventory.ContainsKey("Key"))
            {
                inventory["Key"]--;
                if (inventory["Key"] <= 0)
                {
                    inventory.Remove("Key");
                }
            }

        }
    }




    public void freeze()
    {
        isGameOver = true;
    }

    private void Jump()
    {
        if (!isGameOver)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //Debug.Log("Jump");
        }

    }

    private System.Collections.IEnumerator ResetJumpCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        canJump = true;
        jumpCount = 0;
    }

    public void OnPlatformEnter(PlatformController platform)
    {
        isOnPlatform = true;
        CurrPlatform = platform;
    }

    public void OnPlatformExit(PlatformController platform)
    {
        isOnPlatform = false;
        CurrPlatform = null;

    }

    public void OnSpikeEnter(SpikeController spike)
    {
        isOnSpike = true;
    }

    public void OnSpikeExit(SpikeController spike)
    {
        isOnSpike = false;

    }

    public void OnMonsterEnter(FlyMonsterController monster)
    {
        isOnMonster = true;
    }

    public void OnMonsterExit(FlyMonsterController monster)
    {
        isOnMonster = false;

    }

    public void OnSawEnter(SawController saw)
    {
        isOnMonster = true;
    }

    public void OnSawExit(SawController saw)
    {
        isOnMonster = false;

    }

    private IEnumerator JumpHigherPowerUp()
    {
        jumpForce = 14.0f;
        yield return new WaitForSeconds(5.0f);
        jumpForce = 7.0f;

    }

    private IEnumerator DefrostPowerUp()
    {
        isOnDefrost = true;
        yield return new WaitForSeconds(5.0f);
        isOnDefrost = false;

    }

    private IEnumerator InflictDamages()
    {
        while (true)
        {
            if (isOnPlatform)
            {

                if (currentColour == Color.red)
                {
                    this.TakeDamage(2);
                    speed = 10.0f;
                }

                else if (currentColour == Color.cyan && !isOnDefrost)
                {
                    this.TakeDamage(1);
                    speed = 1.0f;
                }
                else
                {
                    speed = 10.0f;
                }

                yield return new WaitForSeconds(0.5f);
            }

            if (isOnSpike)
            {
                this.TakeDamage(10);
            }

            if (isOnMonster)
            {
                this.TakeDamage(20);
            }

            if (isOnSaw)
            {
                this.TakeDamage(15);
            }

            yield return new WaitForSeconds(0.5f);

        }
    }

    // A new IEnumerator for shielded status
    private IEnumerator ActivateShield()
    {
        isShielded = true;
        yield return new WaitForSeconds(10.0f); // This time can be adjusted
        isShielded = false;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isShielded) // When the player is not shielded
        {
            currentHealth -= damageAmount;
            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                //gameOverText.gameObject.SetActive(true);
                this.freeze();
                gameOverScreen.SetUp();
            }
        }
    }
    private void UpdateHealthUI()
    {
        if (healthText != null && !isGameOver)
        {
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }

            HealthBar.fillAmount = (float)currentHealth / (float)maxHealth;
            healthText.text = "Health: " + currentHealth.ToString() + "/" + maxHealth.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthUp") || collision.CompareTag("Key") || collision.CompareTag("Defrost") || collision.CompareTag("Placeholder") || collision.CompareTag("JumpHigher") || collision.CompareTag("InvincibleShield"))
        {
            string itemName = collision.tag;

            if (inventory.ContainsKey(itemName))
            {
                inventory[itemName]++;
            }
            else
            {
                inventory[itemName] = 1;
            }

            if (itemName == "Key")
            {
                SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;
                Collider2D collider = collision.GetComponent<Collider2D>();
                this.key = collision.GetComponent<KeyGateController>();
                if (collider != null)
                {
                    collider.enabled = false;
                }

            }
            else
            {
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("ColorCollectable"))
        {
            SpriteRenderer collectableSR = collision.GetComponent<SpriteRenderer>();
            Color collectableColor = collectableSR.color;
            this.GetComponent<SpriteRenderer>().color = collectableColor;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Gap"))
        {
            // destroy player object
            this.freeze();
            gameOverScreen.SetUp();
        }

        if (collision.CompareTag("MegaEnhancer"))
        {
            Destroy(collision.gameObject);
            this.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            rb.mass *= playerMassMultiplicationFactor;
            jumpForce *= playerJumpForceMultiplicationFactor;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //Debug.Log("Enter collision");
        canJump = true;
        jumpCount = 0;
    }
}
