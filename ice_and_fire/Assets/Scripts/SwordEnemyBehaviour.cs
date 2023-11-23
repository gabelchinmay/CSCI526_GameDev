using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyBehaviour : MonoBehaviour
{
    private int maxHits = 6;
    public float amplitude = 5f;
    public float frequency = 1 / 2f;
    public string moveDirection = "oscillate";
    public float moveSpeed = 2.0f;
    
    //Initialize Healthbar related component
    [SerializeField] float health, maxHealth = 6.0f;
    private FloatingHealthBar healthBar;

    private int hitCount = 0;
    private bool isAttacking = false;
    private Animator playerAnimator;
    private float previousOscillation = 0f;
    private float speed;

    private Vector3 initialPosition;
    private bool canMove = true;
    private bool isOnFire = false;
    private string swordEnemyType;
    private SendToGoogle sendToGoogle;
    public GameOverScreen gameOverScreen;
    private PlayerController playerController;
    void Start()
    {
        this.swordEnemyType = this.gameObject.tag;
        if (this.swordEnemyType == "spearman")
        {
            this.maxHits = 10;
            this.health = 10f;
            this.maxHealth = 10.0f;
        }
        this.sendToGoogle = FindObjectOfType<SendToGoogle>();
        this.playerController = FindObjectOfType<PlayerController>();
        playerAnimator = GetComponent<Animator>();
        this.healthBar = GetComponentInChildren<FloatingHealthBar>();
        initialPosition = transform.position;
        playerAnimator.SetBool("shoot", false);
        playerAnimator.SetBool("isHurt", false);
        playerAnimator.SetBool("attack", false);
        previousOscillation = amplitude * Mathf.Sin(frequency * Time.time);
        this.speed = this.moveSpeed;
        StartCoroutine(InflictDamages());

    }

    void Update()
    {

        if (canMove && this.moveDirection == "oscillate")
        {

            float oscillation = amplitude * Mathf.Sin(frequency * Time.time);
            //transform.position = initialPosition + Vector3.right * oscillation;
            transform.position = new Vector3(initialPosition.x + oscillation, transform.position.y, transform.position.z);

            playerAnimator.SetFloat("speed", oscillation);

            if (oscillation > previousOscillation)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;

            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            previousOscillation = oscillation;

        }
        else if(canMove && this.moveDirection == "right")
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            playerAnimator.SetFloat("speed", this.speed);
        }
        else if(canMove && this.moveDirection == "left")
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            playerAnimator.SetFloat("speed", this.speed);

        }



        if (isAttacking)
        {
            playerAnimator.SetBool("attack", true);
        }
        else
        {
            playerAnimator.SetBool("attack", false);
        }

    }

    public string getSwordEnemyType()
    {
        return swordEnemyType;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            // moveSpeed = 0f;
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnSwordEnemyEnter(this);
            }
            isAttacking = true;
            
        }

        if (collision.gameObject.CompareTag("DefenseWallSpawn"))
        {
            isAttacking = true;
            canMove = false;

        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            // moveSpeed = 5f;
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnSwordEnemyExit(this);
            }
            isAttacking = false;
        }

        if (collision.gameObject.CompareTag("DefenseWallSpawn"))
        {
            isAttacking = false;
            canMove = true;
        }
    }

    public void TakeHits(int amt)
    {
        
        //isAttacking = false;
        //canMove = false;
        playerAnimator.SetBool("isHurt", true);
        StartCoroutine(resetHurtAnimation());
        hitCount +=amt;
        
        //Update the healthbar
        health -= (float)amt;
        healthBar.UpdateHealthBar(health, maxHealth);
        
        // sword attack count
        if (sendToGoogle != null)
        {
            if (playerController.isSword) { 

            sendToGoogle.ValidSwordAttackCount();

            }
            else if (playerController.isArrow)
            {
                sendToGoogle.HitCount();
            }
        }
        if (hitCount >= maxHits)
        {
            SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
            if(sendToGoogle != null)
            {
                if (playerController.isSword)
                {

                    sendToGoogle.killSwordEnemy();

                }
                else if (playerController.isArrow)
                {
                    sendToGoogle.killEnemy();
                }
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireArrow") || other.CompareTag("IceArrow"))
        {
            Destroy(other.gameObject);
        }

        if (swordEnemyType == "IceSwordEnemy" || swordEnemyType == "Dead" || swordEnemyType == "WhiteWalker" || swordEnemyType == "NightKing")
        {
            if (other.CompareTag("FireArrow"))
            {
                this.TakeHits(2);
               
            }

        }

        if (swordEnemyType == "FireSwordEnemy" || swordEnemyType == "spearman")
        {
            if (other.CompareTag("IceArrow"))
            {
                this.TakeHits(1);
                
            }

        }

        if (other.CompareTag("FireArea"))
        {
            isOnFire = true;
            Debug.Log("On Fire!!!");
        }

        if (other.CompareTag("FireMagic") || other.CompareTag("IceMagic"))
        {
            Destroy(this.gameObject);
            gameOverScreen.SetUp();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FireArea"))
        {
            isOnFire = false;
            Debug.Log("Off Fire!!!");

        }

    }


    private IEnumerator resetHurtAnimation()
    {
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool("isHurt", false);
        //isAttacking = false;
        //canMove = true;
    }


    private IEnumerator InflictDamages()
    {
        while (true)
        {
            if (isOnFire)
            {
                this.TakeHits(1);
                Debug.Log("Sword Enemy damaged");
            }
            yield return new WaitForSeconds(1.0f);

        }
    }

}
