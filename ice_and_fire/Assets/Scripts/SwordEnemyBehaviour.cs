using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyBehaviour : MonoBehaviour
{
    private int hitCount = 0;
    private int maxHits = 3;
    private bool isAttacking = false;
    // Start is called before the first frame update
    private Animator playerAnimator;

    //Private Variables
    private int currIndex = 0;

    //Public Variables
    public Vector2[] setPoints;
    public float movingSpeed = 1.0f;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("attack", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, setPoints[currIndex]) < 0.02f)
        {
            currIndex++;
            if (currIndex >= setPoints.Length)
            {
                currIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, setPoints[currIndex], movingSpeed * Time.deltaTime);

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
    }

    public void TakeHits()
    {
        hitCount++;

        if (hitCount >= maxHits)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("arrow"))
        {
            hitCount++;

            Destroy(other.gameObject);

            if (hitCount >= maxHits)
            {
                Destroy(gameObject);
            }
        }
    }

}
