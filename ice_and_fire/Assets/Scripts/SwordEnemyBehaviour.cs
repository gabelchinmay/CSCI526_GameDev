using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyBehaviour : MonoBehaviour
{
    public int maxHits = 3;
    public float amplitude = 5f;
    public float frequency = 1 / 2f;

    private int hitCount = 0;
    private bool isAttacking = false;
    private Animator playerAnimator;
    private float previousOscillation = 0f;
    private Vector3 initialPosition;
    private bool canMove = true;
    private bool isOnFire = false;
    private string swordEnemyType;
    private SendToGoogle sendToGoogle;

    void Start()
    {
        this.swordEnemyType = this.gameObject.tag;
        this.sendToGoogle = FindObjectOfType<SendToGoogle>();
        playerAnimator = GetComponent<Animator>();
        initialPosition = transform.position;
        playerAnimator.SetBool("shoot", false);
        playerAnimator.SetBool("isHurt", false);
        playerAnimator.SetBool("attack", false);
        previousOscillation = amplitude * Mathf.Sin(frequency * Time.time);
        StartCoroutine(InflictDamages());

    }

    void Update()
    {
        if (canMove)
        {

            float oscillation = amplitude * Mathf.Sin(frequency * Time.time);
            transform.position = initialPosition + Vector3.right * oscillation;
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



        if (isAttacking)
        {
            playerAnimator.SetBool("attack", true);
        }
        else
        {
            playerAnimator.SetBool("attack", false);
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
        // sword attack count
      
        if (hitCount >= maxHits)
        {
            SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
            if(sendToGoogle != null)
            {
                sendToGoogle.killEnemy();
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
                this.TakeHits(1);
            }

        }

        if (swordEnemyType == "FireSwordEnemy")
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
