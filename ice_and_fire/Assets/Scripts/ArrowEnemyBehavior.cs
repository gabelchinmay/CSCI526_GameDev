using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowEnemyBehavior : MonoBehaviour
{
    public string shootDirection = "left";
    public bool canMove = false;
    public GameObject arrowPrefab;
    public float moveSpeed = 2.0f;

    private float shootInterval = 3f;
    private int hitCount = 0;
    private int maxHits = 4;
    private Animator playerAnimator;
    private string arrowEnemyType;
    private bool isOnFire = false;
    private bool canAttack = true;
    private float speed;
    private PlayerController playerController;
    private SendToGoogle sendToGoogle;
    
    //Initialize Healthbar related component
    [SerializeField] float health, maxHealth = 4.0f;
    [SerializeField] FloatingHealthBar healthBar;
    
    void Start()
    {
        this.arrowEnemyType = this.gameObject.tag;
        playerAnimator = GetComponent<Animator>();
        this.healthBar = GetComponentInChildren<FloatingHealthBar>();
        playerAnimator.SetBool("shoot", false);
        playerAnimator.SetBool("isHurt", false);
        playerAnimator.SetBool("attack", false);
        InvokeRepeating("ShootArrow", 0f, shootInterval);
        this.speed = this.moveSpeed;
        StartCoroutine(InflictDamages());
        this.playerController = FindObjectOfType<PlayerController>();
        sendToGoogle = FindObjectOfType<SendToGoogle>();
    }

    void Update()
    {

        if (this.shootDirection == "right" && canMove)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            playerAnimator.SetFloat("speed", this.speed);
        }
        else if (this.shootDirection == "left" && canMove)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            playerAnimator.SetFloat("speed", this.speed);

        }


    }

    public string getarrowEnemyType()
    {
        return arrowEnemyType;
    }

    void ShootArrow()
    {
        if (canAttack) {
            playerAnimator.SetFloat("speed", 0f);
            playerAnimator.SetBool("shoot", true);
            if (shootDirection == "left") {

                Vector3 offset = transform.position + Vector3.up * 1f + Vector3.left * 2f;
                arrowPrefab.GetComponent<SpriteRenderer>().flipX = true;
                GameObject arrow = Instantiate(arrowPrefab, offset, Quaternion.identity);

                Rigidbody2D a = arrow.GetComponent<Rigidbody2D>();
                a.velocity = new Vector2(-15f, 0); // Shooting to the left

            }
            else
            {
                Vector3 offset = transform.position + Vector3.up * 1f + Vector3.right * 2f;
                GameObject arrow = Instantiate(arrowPrefab, offset, Quaternion.identity);
                // change the arrow's target
                arrow.transform.eulerAngles = new Vector3(0, 180, 0);
                Rigidbody2D a = arrow.GetComponent<Rigidbody2D>();
                a.velocity = new Vector2(15f, 0); // Shooting to the right
            }
            


        }


    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("FireArrow") || other.CompareTag("IceArrow"))
        {
            Destroy(other.gameObject);
        }

        if (arrowEnemyType == "IceArrowEnemy")
        {
            if (other.CompareTag("FireArrow"))
            {
                TakeHits(2);
                
            }

        }

        if (arrowEnemyType == "FireArrowEnemy")
        {
            if (other.CompareTag("IceArrow"))
            {
                TakeHits(1);

                
            }

        }




        if (other.CompareTag("FireArea"))
        {
            isOnFire = true;
        }


    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FireArea"))
        {
            isOnFire = false;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnArrowEnemyEnter(this);
            }

        }


    }


    private void OnCollisionExit2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnArrowEnemyExit(this);
            }
        }
    }

    public void TakeHits(int amt)
    {
        hitCount+=amt;
        
        //Update the healthbar
        health -= (float)amt;
        healthBar.UpdateHealthBar(health, maxHealth);

        playerAnimator.SetBool("shoot", false);
        playerAnimator.SetBool("isHurt", true);
        this.canAttack = false;
        StartCoroutine(resetHurtAnimation());
        if (sendToGoogle != null)
        {
            if (playerController.isSword)
            {

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
            if (sendToGoogle != null)
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



    private IEnumerator InflictDamages()
    {
        while (true)
        {
            if (isOnFire)
            {
                this.TakeHits(1);
                Debug.Log("Arrow Enemy damaged");
            }
            yield return new WaitForSeconds(1.0f);

        }
    }

    private IEnumerator resetHurtAnimation()
    {
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool("shoot", true);
        playerAnimator.SetBool("isHurt", false);
        this.canAttack = true;


    }

}
