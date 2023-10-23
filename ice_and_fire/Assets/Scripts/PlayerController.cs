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
    private float jumpForce;
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
    public TMP_Text pickUpText;
    public TMP_Text shieldTimerText;
    public TMP_Text defrostTimerTxt;
    public Image HealthSkeleton;
    public Image HealthBar;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    private Vector3 playerPreviousPosition;
    public GameObject placeholderPrefab;
    public GameObject defenseWallPrefab;
    private bool isOnDefrost = false;
    private KeyGateController key = null;
    private bool isValyrian = false;

    // new Feature: Invicible Shield, boolean variable to show if this player is shielded or not
    private bool isShielded = false;
    private bool isOnSpike = false;
    private bool isOnMonster = false;
    private bool isOnSaw = false;
    //private bool isGettingSwordHits = false;
    //private bool isNearArrowEnemy = false;
    public float playerMassMultiplicationFactor = 100f;
    public float playerJumpForceMultiplicationFactor = 100f;

    public GameOverScreen gameOverScreen;
    private bool isMovingToPosition = false;

    private Animator playerAnimator;


    public GameObject arrowPrefab;
    public GameObject wildFirePrefab;

    private int direction = 1;

    private ArrowEnemyBehavior currentArrowEnemyPlayerFighting = null;
    private SwordEnemyBehaviour currentSwordEnemyPlayerFighting = null;
    private bool canSwordAttack = true;
    private bool canArrowAttack = true;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(InflictDamages());
        StartCoroutine(InflictDamagesFromAntagonists());
        playerAnimator.SetBool("attack", false);
        playerAnimator.SetBool("shoot", false);
        playerAnimator.SetBool("isHurt",false);
    }

    void Update()
    {
        //Testing attack animation only:

        if (Input.GetKeyDown(KeyCode.T) && canSwordAttack)
        {
            canSwordAttack = false;

            playerAnimator.SetBool("attack", true);
            StartCoroutine(resetSwordAttackAnimation());

            if(currentArrowEnemyPlayerFighting != null)
            {
                currentArrowEnemyPlayerFighting.TakeHits();
            }

            if (currentSwordEnemyPlayerFighting != null)
            {
                currentSwordEnemyPlayerFighting.TakeHits();
            }

            StartCoroutine(swordAttackCooldownRoutine());


        }
        
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

            }
        }

        // Movements
        if (!isGameOver)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            playerAnimator.SetFloat("speed",Math.Abs(horizontalInput * speed));


            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if (horizontalInput < 0)
            {
                spriteRenderer.flipX = true;
                direction = -1;

            }
            else
            {
                spriteRenderer.flipX = false;
                direction = 1;
            }




        }
        else
        {
            rb.velocity = Vector2.zero;
        }



        if (inventory.Count != 0)
        {
            InventoryText.text = "";
            InventoryText.text += "INVENTORY";

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
            if (inventory.ContainsKey("DefenseWall"))
            {
                InventoryText.text += "\nDefenseWalls: " + inventory["DefenseWall"];
            }

        }
        else
        {
            InventoryText.text = "";

        }

        if (Input.GetKeyDown(KeyCode.K) && inventory.ContainsKey("Placeholder") && !isMovingToPosition)
        {

            playerPreviousPosition = transform.position;
            Vector3 placeholderPosition = playerPreviousPosition - Vector3.up * 0.7f;
            
            if (rb.mass < 9)
            {
                Vector2 targetPosition = rb.position + Vector2.right * 1.1f;
                StartCoroutine(MovePlayerToPositionAndPlacePlaceHolder(targetPosition, speed, placeholderPosition));
            }
            else
            {

                Vector2 targetPosition = rb.position + Vector2.right * 1.4f;
                StartCoroutine(MovePlayerToPositionAndPlacePlaceHolder(targetPosition, speed, placeholderPosition));
            }



        }


        if (Input.GetKeyDown(KeyCode.J) && inventory.ContainsKey("DefenseWall"))
        {
            DefenseWallPowerUp();

            if (inventory.ContainsKey("DefenseWall"))
            {
                inventory["DefenseWall"]--;
                if (inventory["DefenseWall"] <= 0)
                {
                    inventory.Remove("DefenseWall");
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


        if (Input.GetKeyDown(KeyCode.S) && canArrowAttack) // Replace with your preferred shoot key.
        {
            canArrowAttack = false;
            playerAnimator.SetBool("shoot", true);
            StartCoroutine(resetBowAttackAnimation());
            StartCoroutine(ShootArrow(arrowPrefab));
            StartCoroutine(bowAttackCooldownRoutine());
        }
    }

    IEnumerator swordAttackCooldownRoutine()
    {
        yield return new WaitForSeconds(1);
        canSwordAttack = true; // Reset the flag after cooldown
    }

    IEnumerator bowAttackCooldownRoutine()
    {
        yield return new WaitForSeconds(1);
        canArrowAttack = true; // Reset the flag after cooldown
    }


    private IEnumerator MovePlayerToPositionAndPlacePlaceHolder(Vector2 targetPosition, float speed, Vector3 placeholderPosition)
    {
        float startTime = Time.time;
        Vector2 startPosition = rb.position;
        isMovingToPosition = true;

        while (Time.time - startTime < 0.5f)
        {
            Vector2 direction = (targetPosition - rb.position).normalized;

            // Set the velocity to move the player in the specified direction with the given speed
            rb.velocity = direction * speed;

            // Check if the player has reached the target position
            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                rb.velocity = Vector2.zero;
                rb.position = targetPosition;
                break; // Exit the loop
            }


        yield return null;
        }

        isMovingToPosition = false;

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




    public void freeze()
    {
        isGameOver = true;
    }

    private void Jump()
    {
        if (!isGameOver)
        {
            float mass = rb.mass;
            if (jumpCount < 1) jumpForce = 7.0f * mass;
            else jumpForce = 5.0f * mass; //Reduce the jump force for second jump
            
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
        isOnSaw = true;
    }

    public void OnSawExit(SawController saw)
    {
        isOnSaw = false;

    }

    public void OnSwordEnemyEnter(SwordEnemyBehaviour swordEnemy)
    {
        //isGettingSwordHits = true;
        currentSwordEnemyPlayerFighting = swordEnemy;
    }

    public void OnSwordEnemyExit(SwordEnemyBehaviour swordEnemy)
    {
        //isGettingSwordHits = false;
        currentSwordEnemyPlayerFighting = null;

    }

    public void OnArrowEnemyEnter(ArrowEnemyBehavior arrowEnemy)
    {
        //isNearArrowEnemy = true;
        currentArrowEnemyPlayerFighting = arrowEnemy;
    }

    public void OnArrowEnemyExit(ArrowEnemyBehavior arrowEnemy)
    {
        //isGettingSwordHits = false;
        currentArrowEnemyPlayerFighting = null;

    }

    private void DefenseWallPowerUp()
    {
        Vector3 offset = transform.position + Vector3.up * 0.40f + Vector3.right * 2f;
        Instantiate(defenseWallPrefab,offset,Quaternion.identity);

    }

    private IEnumerator DefrostPowerUp()
    {
        isOnDefrost = true;
        StartCoroutine(DefrostTimer(5.0f));
        yield return new WaitForSeconds(5.0f);
        isOnDefrost = false;

    }

    private IEnumerator resetSwordAttackAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        playerAnimator.SetBool("attack", false);

    }


    private IEnumerator resetBowAttackAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        playerAnimator.SetBool("shoot", false);

    }

    private IEnumerator DefrostTimer(float time)
    {
        while (time > 0)
        {
            defrostTimerTxt.text = "Defrost: " + Mathf.Ceil(time).ToString();
            time -= Time.deltaTime;

            yield return null;
        }
        defrostTimerTxt.gameObject.SetActive(false);

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
            }


            yield return new WaitForSeconds(1.0f);

        }
    }

    private IEnumerator InflictDamagesFromAntagonists() {
        while (true)
        {

            if (isOnSpike)
            {
                this.TakeDamage(1);
            }

            if (isOnMonster)
            {
                this.TakeDamage(2);
            }

            if (isOnSaw)
            {
                this.TakeDamage(1);
            }
            if (currentSwordEnemyPlayerFighting != null)
            {
                this.TakeDamage(1);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    // A new IEnumerator for shielded status
    private IEnumerator ActivateShield(float duration)
    {
        if (isShielded)
        {
            yield break;
        }

        isShielded = true;
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            shieldTimerText.text = "Shield: " + Mathf.Ceil(remainingTime).ToString();
            remainingTime -= Time.deltaTime;

            yield return null;
        }

        isShielded = false;

        shieldTimerText.gameObject.SetActive(false);
    }



    public void TakeDamage(int damageAmount)
    {
        Debug.Log("isShielded: " + isShielded);

        if (!isShielded) // When the player is not shielded
        {
            currentHealth -= damageAmount;
            UpdateHealthUI();
            
            playerAnimator.SetBool("isHurt",true);
            
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
        if (collision.CompareTag("Key") || collision.CompareTag("Defrost") || collision.CompareTag("Placeholder") || collision.CompareTag("DefenseWall"))
        {

            string itemName = collision.tag;
            StartCoroutine(DisplayTextForDuration("Picked up "+itemName+"!", 3.0f, Color.yellow));


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

        if (collision.CompareTag("InvincibleShield"))
        {
            StartCoroutine(ActivateShield(10.0f));
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("HealthUp"))
        {
            StartCoroutine(DisplayTextForDuration("+10 HP", 3.0f, Color.green));
            if (currentHealth <= maxHealth - 10)
            {
                currentHealth += 10;

            }
            else
            {
                currentHealth = maxHealth;
            }
            UpdateHealthUI();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("ColorCollectable"))
        {
            StartCoroutine(DisplayTextForDuration("Player changed color! ", 3.0f, Color.yellow));
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
            StartCoroutine(DisplayTextForDuration("Picked up MegaEnhancer!", 3.0f, Color.yellow));
            Destroy(collision.gameObject);
            this.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            rb.mass *= playerMassMultiplicationFactor;
            jumpForce *= playerJumpForceMultiplicationFactor;
        }

        if (collision.CompareTag("arrow"))
        {
            StartCoroutine(DisplayTextForDuration("-2 HP", 1f, Color.red));
            currentHealth -= 2;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }

            UpdateHealthUI();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("ValyrianSword"))
        {
            Destroy(collision.gameObject);
            this.isValyrian = true;
        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Reset jump count only if colliding with "Platform" and "Antagonist"
        bool onPlatform = (other.gameObject.CompareTag("Platform_Normal")||other.gameObject.CompareTag("Platform_Moving")||other.gameObject.CompareTag("Platform_Rotate")||other.gameObject.CompareTag("Platform_AutoSpin")
                           ||other.gameObject.CompareTag("Platform_Breakable")||other.gameObject.CompareTag("Platform_Color")||other.gameObject.CompareTag("Platform_Tri") || other.gameObject.CompareTag("DefenseWallSpawn"));
        bool onAntagonist = (isOnMonster || isOnSaw || isOnSpike);
        if (onPlatform || onAntagonist)
        {
            canJump = true;
            jumpCount = 0;
            
            //Disable the Jumping animation when landing
            playerAnimator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        bool offPlatform = (other.gameObject.CompareTag("Platform_Normal") || other.gameObject.CompareTag("Platform_Moving") || other.gameObject.CompareTag("Platform_Rotate") || other.gameObject.CompareTag("Platform_AutoSpin")
                   || other.gameObject.CompareTag("Platform_Breakable") || other.gameObject.CompareTag("Platform_Color") || other.gameObject.CompareTag("Platform_Tri") || other.gameObject.CompareTag("DefenseWallSpawn"));
        bool offAntagonist = (isOnMonster || isOnSaw || isOnSpike);
        
        //Disable double jump on rotating platform
        if (other.gameObject.CompareTag("Platform_Rotate") || other.gameObject.CompareTag("Platform_AutoSpin"))
        {
            jumpCount++;
        }

        if (offPlatform)
        {
            playerAnimator.SetBool("isJumping", true);
        }

        if (offAntagonist)
        {
            float countDown = 1.0f;
            while (countDown > 0)
            {
                countDown -= (0.5f * Time.deltaTime);
            }
            playerAnimator.SetBool("isHurt", false);
        }
    }

    private IEnumerator DisplayTextForDuration(string message, float duration, Color c)
    {
        pickUpText.text = message;
        pickUpText.color = c;
        yield return new WaitForSeconds(duration);
        pickUpText.text = "";
    }


    private IEnumerator ShootArrow(GameObject arrowType)
    {
        Vector3 offset;
        GameObject arrow;
        Rigidbody2D a;
        yield return new WaitForSeconds(0.8f);

        if (this.direction == -1)
        {
            offset = transform.position + Vector3.up * 1f + Vector3.left * 2f;
            arrow = Instantiate(arrowType, offset, Quaternion.identity);
            arrow.GetComponent<SpriteRenderer>().flipX =true;
            a = arrow.GetComponent<Rigidbody2D>();
            a.velocity = new Vector2(35f * this.direction, 0);

        }
        else
        {
            offset = transform.position + Vector3.up * 1f + Vector3.right * 2f;
            arrow = Instantiate(arrowType, offset, Quaternion.identity);
            a = arrow.GetComponent<Rigidbody2D>();
            a.velocity = new Vector2(35f * this.direction, 0);
        }

    }


}
