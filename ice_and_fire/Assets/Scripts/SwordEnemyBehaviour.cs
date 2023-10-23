using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyBehaviour : MonoBehaviour
{
    private int hitCount = 0;
    private int maxHits = 3;
    private bool isAttacking = false;
    private Animator playerAnimator;
    private float previousOscillation = 0f;

    public float amplitude = 5f; 
    public float frequency = 1 / 2f; 


    private Vector3 initialPosition;

    private bool canMove = true;


    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        initialPosition = transform.position;
        playerAnimator.SetBool("attack", true);
        previousOscillation = amplitude * Mathf.Sin(frequency * Time.time);

    }

    void Update()
    {
        if (canMove)
        {

            float oscillation = amplitude * Mathf.Sin(frequency * Time.time);
            transform.position = initialPosition + Vector3.right * oscillation;


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
