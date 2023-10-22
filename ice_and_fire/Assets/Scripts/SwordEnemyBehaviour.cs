using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    private int hitCount = 0;
    private int maxHits = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            moveSpeed = 0;
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnSwordEnemyEnter(this);
            }
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
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "arrow"
        if (other.CompareTag("arrow"))
        {
            // Increase the hit count
            hitCount++;

            // Destroy the arrow that hit the enemy
            Destroy(other.gameObject);

            // Check if hit count reached max hits
            if (hitCount >= maxHits)
            {
                Destroy(gameObject);
            }
        }
    }

}
