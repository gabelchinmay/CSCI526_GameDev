using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    private int hitCount = 0;
    private int maxHits = 3;
    private bool isAttacking = false;
    // Start is called before the first frame update
    private Animator playerAnimator;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("attack", true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

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
            moveSpeed = 0f;
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
            moveSpeed = 5f;
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
