using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    //public Variables
    public bool isArrow = false;
    public bool isSword = false;
    public int maxHealth = 100;
    public int killCount = 0;
    public string playerStyle = "fire";
    public TMP_Text healthText;
    public TMP_Text gameOverText;
    public TMP_Text pickUpText;
    public TMP_Text shieldTimerText;
    public Image HealthSkeleton;
    public Image HealthBar;
    public GameObject fireArrowPrefab;
    public GameObject iceArrowPrefab;
    public GameObject defenseWallPrefab;
    public GameOverScreen gameOverScreen;
    public Sprite modRenderer;
    public Sprite edRenderer;
    public RuntimeAnimatorController modAnimator;
    public RuntimeAnimatorController edAnimator;


    //private variables
    private bool isGameOver = false;
    private bool canJump = true;
    private bool isOnPlatform = false;
    private bool isShielded = false;
    private bool isOnSpike = false;
    private bool isOnMonster = false;
    private bool isOnSaw = false;
    private bool isOnFireDragon = false;
    private bool isOnIceDragon = false;
    private bool canSwordAttack = true;
    private bool canArrowAttack = true;
    private bool isInColdArea = false;
    private bool isInNightKingArea = false;
    private bool usingFireSword = false;
    private bool usingIceSword = false;
    private bool usingFireArrows = false;
    private bool usingIceArrows = false;
    private int jumpCount;
    private int maxJumps = 1;
    private int currentHealth;
    private int direction = 1;
    private float speed = 8.0f;
    private float jumpForce = 10.0f;
    private float playerMassMultiplicationFactor = 2f;
    private float playerJumpForceMultiplicationFactor = 2f;
    private Rigidbody2D rb;
    private PlatformController CurrPlatform = null;
    private Color currentColour;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    private Animator playerAnimator;
    private ArrowEnemyBehavior currentArrowEnemyPlayerFighting = null;
    private SwordEnemyBehaviour currentSwordEnemyPlayerFighting = null;
    private SendToGoogle sendToGoogle;
    private GateController gateController;
    private KeyGateController keyGateController;
    private SpriteRenderer spriteRenderer;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        SetActivePlayer("fire");
        
        

    }

    void Start()
    {
        this.sendToGoogle = FindObjectOfType<SendToGoogle>();
        this.gateController = FindObjectOfType<GateController>();
        this.keyGateController = FindObjectOfType<KeyGateController>();

        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthUI();
        //StartCoroutine(InflictDamagesFromPlatform());
        StartCoroutine(InflictDamagesFromAntagonists());
        StartCoroutine(InflictColdDamage());
        InitAnimations();
        InitPlayerWepons();
        //HandleWildFireArrows();
    }

    void Update()
    {   
        this.rb = GetComponent<Rigidbody2D>();
        HandleIceFireJumpEffects();
        //UpdateInventoryTxt();
        MovePlayer();
        ButtonControls();
        MODpassFireGate();// open gate
        EDWpassFireGate(); //EDW unlock door
        updateJumpAnimation(); //set isJumping to true when velocity.y != 0
        switchPlayerStyle();
        NullPointCheck();
    }

    private void InitAnimations()
    {
        playerAnimator.SetBool("attack", false);
        playerAnimator.SetBool("fireSwordAttack", false);
        playerAnimator.SetBool("iceSwordAttack", false);
        playerAnimator.SetBool("shoot", false);
        playerAnimator.SetBool("isHurt", false);
    }

    private void InitPlayerWepons() {

        if (this.playerStyle == "fire")
        {
            this.usingFireArrows = true;
            this.usingFireSword = true;
            this.usingIceArrows = false;
            this.usingIceSword = false;
        }
        else
        {
            this.usingFireArrows = false;
            this.usingFireSword = false;
            this.usingIceArrows = true;
            this.usingIceSword = true;

        }


    }


    public void NullPointCheck()
    {
        if (keyGateController != null)
        {
            keyGateController.passStyle(playerStyle);
        }

        // Check if gateController is assigned before using it
        if (gateController != null)
        {
            gateController.passFireStyle(playerStyle);
        }
    }


    public void MODpassFireGate()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("MOD");
        foreach (GameObject obj in objectsWithTag)
        {
            
        }
    }

    public void EDWpassFireGate()
    {

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("EDW");
        foreach (GameObject obj in objectsWithTag)
        {
            
        }
        
    }

    private void switchPlayerStyle()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // For MOD
            this.playerStyle = "fire";
            this.usingFireArrows = true;
            this.usingFireSword = true;
            this.usingIceArrows = false;
            this.usingIceSword = false;

            SetActivePlayer("fire");
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            // For Ed
            this.playerStyle = "ice";
            this.usingFireArrows = false;
            this.usingFireSword = false;
            this.usingIceArrows = true;
            this.usingIceSword = true;
            SetActivePlayer("ice");


        }


    }



    private void SetActivePlayer(string playerStyle)
    {
        if(playerStyle == "fire")
        {
            spriteRenderer.sprite = modRenderer;
            playerAnimator.runtimeAnimatorController = modAnimator;

        }



        if(playerStyle == "ice")
        {
            spriteRenderer.sprite = edRenderer;
            playerAnimator.runtimeAnimatorController = edAnimator;
        }
    }



    public string getPlayerStyle() {
        return this.playerStyle;

    }


    private void MovePlayer()
    {
        if (!isGameOver)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            playerAnimator.SetFloat("speed", Math.Abs(horizontalInput * speed));


            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if (horizontalInput < 0)
            {
                spriteRenderer.flipX = true;
                this.direction = -1;

            }
            else if(horizontalInput > 0)
            {
                spriteRenderer.flipX = false;
                this.direction = 1;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void updateJumpAnimation()
    {
        float dropping = rb.velocity.y;
        if (dropping == 0)
        {
            playerAnimator.SetBool("isJumping", false);
        }
        else
        {
            playerAnimator.SetBool("isJumping", true);
        }
    }

    private void ButtonControls()
    {



        // Defense Wall
        if (Input.GetKeyDown(KeyCode.D) && inventory.ContainsKey("DefenseWall"))
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




        // Arrow-
        if (Input.GetKeyDown(KeyCode.V) && canArrowAttack) // Replace with your preferred shoot key.
        {

            pressArrow();
            if (isArrow)
            {
                sendToGoogle.ShootArrow();
            }
             
            //{
                if (this.sendToGoogle != null)
                {
                    this.sendToGoogle.ShootArrow();
                }
                
                //Testing fire arrow attack & ice arrow attack animation - Ashley 10.26 13:00
                if (usingFireArrows)
                {
                    playerAnimator.SetBool("fireArrowShoot", true);
                    StartCoroutine(ShootArrow(fireArrowPrefab));

                }
                else if (usingIceArrows)
                {

                    playerAnimator.SetBool("iceArrowShoot", true);
                    StartCoroutine(ShootArrow(iceArrowPrefab));
            }

            StartCoroutine(resetBowAttackAnimation());

            canArrowAttack = false;

            StartCoroutine(bowAttackCooldownRoutine());
        }
        
        //Sword
        if (Input.GetKeyDown(KeyCode.C) && canSwordAttack)
        {

            pressSword();
            if (isSword)
            {
                sendToGoogle.SwordWavedCount();
            }

            if (usingFireSword == true || usingIceSword == true)
            {
                canSwordAttack = false;
                
                if (usingFireSword)
                {
                    playerAnimator.SetBool("fireSwordAttack", true);
                }
                else if (usingIceSword)
                {
                    playerAnimator.SetBool("iceSwordAttack", true);
                }

                StartCoroutine(resetSwordAttackAnimation());


            }


            if(usingIceSword == true)
            {

                if (currentArrowEnemyPlayerFighting != null && currentArrowEnemyPlayerFighting.CompareTag("FireArrowEnemy"))
                {
                    currentArrowEnemyPlayerFighting.TakeHits(2);
                }

                if (currentSwordEnemyPlayerFighting != null && currentSwordEnemyPlayerFighting.CompareTag("FireSwordEnemy"))
                {
                    currentSwordEnemyPlayerFighting.TakeHits(2);
                }

            }


            if (usingFireSword == true)
            {
                if (currentArrowEnemyPlayerFighting != null && currentArrowEnemyPlayerFighting.CompareTag("IceArrowEnemy"))
                {
                    currentArrowEnemyPlayerFighting.TakeHits(1);

                }

                if (currentSwordEnemyPlayerFighting != null && currentSwordEnemyPlayerFighting.CompareTag("IceSwordEnemy"))
                {
                    currentSwordEnemyPlayerFighting.TakeHits(1);
                }

                if (currentSwordEnemyPlayerFighting != null && currentSwordEnemyPlayerFighting.CompareTag("Dead"))
                {
                    currentSwordEnemyPlayerFighting.TakeHits(1);
                }

                if (currentSwordEnemyPlayerFighting != null && currentSwordEnemyPlayerFighting.CompareTag("WhiteWalker"))
                {
                    currentSwordEnemyPlayerFighting.TakeHits(1);
                }

                if (currentSwordEnemyPlayerFighting != null && currentSwordEnemyPlayerFighting.CompareTag("NightKing"))
                {
                    currentSwordEnemyPlayerFighting.TakeHits(1);
                    Debug.Log("Get hurt");
                }
            }

            StartCoroutine(swordAttackCooldownRoutine());
        }

    }


    private void pressArrow()
    {
        isArrow = true;
        isSword = false;
    }

    private void pressSword()
    {
        isArrow = false;
        isSword = true;
    }

    private void MODopenDoor()
    {

    }

    private void HandleIceFireJumpEffects()
    {
        currentColour = Color.black;

        if (CurrPlatform != null)
        {
            currentColour = CurrPlatform.getSpriteColor();
        }
        if (!isOnPlatform)
        {
            //speed = 10.0f;
            canJump = true;
        }
        else
        {
            if ( (currentColour == Color.cyan && this.playerStyle != "ice")) //removed defrost
            {
                canJump = false;
            }
            else if (currentColour == Color.red )
            {
                canJump = true;
            }
            else
            {
                canJump = true;
            }
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps && canJump)
        {
            Jump();
            jumpCount++;
            if(sendToGoogle!= null)
            {
                sendToGoogle.addJump();
            }
            if (jumpCount == maxJumps)
            {
                canJump = false;
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DefenseWall"))

        {

            string itemName = collision.tag;

            StartCoroutine(DisplayTextForDuration("Picked up " + itemName + "!", 3.0f, Color.yellow));




        if (inventory.ContainsKey(itemName))
            {
                inventory[itemName]++;
            }
        else
            {
                inventory[itemName] = 1;
            }

                Destroy(collision.gameObject);
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


        if (collision.CompareTag("Gap"))
        {
            this.freeze();
            gameOverScreen.SetUp();
        }

        if (collision.CompareTag("DragonEgg") && this.playerStyle == "fire")
        {
            StartCoroutine(DisplayTextForDuration("Picked up a Dragon egg!", 3.0f, Color.yellow));
            Destroy(collision.gameObject);
            if (sendToGoogle != null)
            {
                sendToGoogle.pickUpEgg();
                sendToGoogle.threeEggSend();
            }
            rb.mass *= playerMassMultiplicationFactor;
            jumpForce *= playerJumpForceMultiplicationFactor;
        }

        if (collision.CompareTag("arrow"))
        {       
            this.TakeDamage(2);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("FireArea"))
        {
            Debug.Log("Player on fire!!!!!");
        }

        if (collision.CompareTag("ColdArea"))
        {
            isInColdArea = true;
        }

        if (collision.CompareTag("NightKingArea"))
        {
            isInNightKingArea = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ColdArea"))
        {
            isInColdArea = false;
        }

        if (collision.CompareTag("NightKingArea"))
        {
            isInNightKingArea = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Reset jump count only if colliding with "Platform" and "Antagonist"
        bool onPlatform = (other.gameObject.CompareTag("Platform_Normal") || other.gameObject.CompareTag("Platform_Moving") || other.gameObject.CompareTag("Platform_Rotate") || other.gameObject.CompareTag("Platform_AutoSpin")
                           || other.gameObject.CompareTag("Platform_Breakable") || other.gameObject.CompareTag("Platform_Color") || other.gameObject.CompareTag("Platform_Tri") || other.gameObject.CompareTag("DefenseWallSpawn"));
        bool onAntagonist = (isOnMonster || isOnSaw || isOnSpike);
        bool isTouchDragon = (other.gameObject.CompareTag("FireDragon") || other.gameObject.CompareTag("IceDragon"));
        if (onPlatform || onAntagonist)
        {
            canJump = true;
            jumpCount = 0;

            //Disable the Jumping animation when landing
            playerAnimator.SetBool("isJumping", false);
        }

        // If the player has a contact with the dragon, this player will be killed
        if (isTouchDragon)
        {
            this.freeze();
            gameOverScreen.SetUp();
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

        // if (offPlatform)
        // {
        //     //playerAnimator.SetBool("isJumping", true);
        // }

        float countDown = 1.0f;
        while (countDown > 0)
        {
            countDown -= (0.5f * Time.deltaTime);
        }
        playerAnimator.SetBool("isHurt", false);
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
        }
    }

    private void DefenseWallPowerUp()
    {
        if(this.direction != -1)
        {
            Vector3 offset = transform.position + Vector3.up * 0.4f + Vector3.right * 2f;
            Instantiate(defenseWallPrefab, offset, Quaternion.identity);

        }
        else
        {
            Vector3 offset = transform.position + Vector3.up * 0.4f + Vector3.left * 2f;
            Instantiate(defenseWallPrefab, offset, Quaternion.identity);
        }

    }

    public void TakeDamage(int damageAmount)
    {
        if (!isShielded) 
        {
            currentHealth -= damageAmount;
            sendToGoogle.HealthStatus(damageAmount);
            UpdateHealthUI();
            //StartCoroutine(DisplayTextForDuration("-"+ damageAmount.ToString() +" HP", 1f, Color.red));
            playerAnimator.SetBool("isHurt", true);
            StartCoroutine(resetHurtAnimation());
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




    private IEnumerator swordAttackCooldownRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        canSwordAttack = true; // Reset the flag after cooldown
    }

    private IEnumerator bowAttackCooldownRoutine()
    {
        yield return new WaitForSeconds(1f);
        canArrowAttack = true; // Reset the flag after cooldown
    }



    private IEnumerator ResetJumpCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        canJump = true;
        jumpCount = 0;
    }



    private IEnumerator resetSwordAttackAnimation()
    {
        //Need to confirm with the cool down time (1.0 -> 0.5) - Ashley
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("attack", false);
        playerAnimator.SetBool("fireSwordAttack", false);
        playerAnimator.SetBool("iceSwordAttack", false);
    }


    private IEnumerator resetBowAttackAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        playerAnimator.SetBool("shoot", false);
        playerAnimator.SetBool("fireArrowShoot", false);
        playerAnimator.SetBool("iceArrowShoot", false);

    }


    private IEnumerator resetHurtAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("isHurt", false);

    }


    //private IEnumerator InflictDamagesFromPlatform()
    //{
    //    while (true)
    //    {
    //        Debug.Log((currentColour == Color.red).ToString() +  this.playerStyle + isOnPlatform);



    //        if (isOnPlatform)
    //        {
    //            currentColour = CurrPlatform.getSpriteColor();


    //            if (currentColour == Color.red && this.playerStyle != "fire")
    //            {
    //                this.TakeDamage(10);
    //                //speed = 10.0f;
    //            }

    //            else if (currentColour == Color.cyan  && this.playerStyle != "ice")
    //            {
    //                this.TakeDamage(1);
    //                //speed = 1.0f;
    //            }
    //            else
    //            {
    //                //speed = 10.0f;
    //            }
    //        }


    //        yield return new WaitForSeconds(1.0f);

    //    }
    //}

    private IEnumerator InflictDamagesFromAntagonists()
    {
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

            if (isOnFireDragon)
            {
                TakeDamage(2);
                
            }
            if (isOnIceDragon)
            {
                TakeDamage(1);
                speed = 1.0f;
                
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    private IEnumerator InflictColdDamage()
    {
        while (true)
        {
            if (isInColdArea)
            {
                TakeDamage(1);
            }

            if (isInNightKingArea)
            {
                TakeDamage(3);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }


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
            offset = transform.position + Vector3.up * 0.5f + Vector3.left * 3.5f;
            arrow = Instantiate(arrowType, offset, Quaternion.identity);
            arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            a = arrow.GetComponent<Rigidbody2D>();
            a.velocity = new Vector2(35f * this.direction, 0);

        }
        else
        {
            offset = transform.position + Vector3.up * 0.5f + Vector3.right * 3.5f;
            arrow = Instantiate(arrowType, offset, Quaternion.identity);
            a = arrow.GetComponent<Rigidbody2D>();
            a.velocity = new Vector2(35f * this.direction, 0);
        }
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

    // implement dragons' damage to players
    public void OnDragonFireEnter(FireBehaviour fire)
    {
        if (this.playerStyle != "fire"){
            this.TakeDamage(15);
            isOnFireDragon = true;
        }
        
    }

    public void OnDragonFireExit(FireBehaviour fire)
    {
        isOnFireDragon = false;
    }

    public void OnIceDragonFireEnter(IceFireBehaviour iceFire)
    {
        if (this.playerStyle != "ice"){
            this.TakeDamage(17);
            isOnIceDragon = true;
        }
    }

    public void OnIceDragonFireExit(IceFireBehaviour iceFire)
    {
        isOnIceDragon = false;
    }


}
