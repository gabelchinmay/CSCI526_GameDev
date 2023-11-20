using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearEnemyBehavior : MonoBehaviour
{
    public int maxHits = 6;
    
    //Initialize Healthbar related component
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] FloatingHealthBar healthBar;

    private string spearEnemyType;
    private int hitCount = 0;
    private bool isOnFire = false;
    private bool isAttacking = false;

    // moving
    private bool canMove = true;
    private Animator playerAnimator;
    private Vector3 initialPosition;
    public float amplitude = 2f;
    public float frequency = 1 / 2f;
    private float previousOscillation = 0f;
    public string moveDirection = "oscillate";
    public float moveSpeed = 2.0f;
    private float speed;



    // Start is called before the first frame update
    void Start()
    {
        this.spearEnemyType = this.gameObject.tag;
        this.healthBar = GetComponentInChildren<FloatingHealthBar>();

        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("shoot", false);
        playerAnimator.SetBool("isHurt", false);
        playerAnimator.SetBool("attack", false);

        initialPosition = transform.position;
        this.speed = this.moveSpeed;

        health = 6.0f;
        maxHealth = 6.0f;

    }

    // Update is called once per frame
    void Update()
    {
        // move back and forth
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


    }

    public void TakeHits(int amt)
    {
        hitCount += amt;
        health = health - amt;

        Debug.Log(health);

        healthBar.UpdateHealthBar(health,maxHealth);
        StartCoroutine(resetHurtAnimation());
        
        
        if (hitCount >= maxHits)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireArrow") || other.CompareTag("IceArrow"))
        {
            Destroy(other.gameObject);
            
        }


        if (spearEnemyType == "FireSpear")
        {
            if (other.CompareTag("IceArrow"))
            {
                this.TakeHits(1);
                Debug.Log("Hit by ice arrow!");
                
            }

        }

        if (other.CompareTag("FireArea"))
        {
            isOnFire = true;
            Debug.Log("On Fire!!!");
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
    }


    private IEnumerator InflictDamages()
    {
        while (true)
        {
            if (isOnFire)
            {
                this.TakeHits(1);
                // Debug.Log("Spear Enemy damaged");
            }
            yield return new WaitForSeconds(1.0f);

        }
    }
}
