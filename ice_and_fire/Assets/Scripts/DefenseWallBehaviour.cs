using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWallBehaviour : MonoBehaviour
{
    private int hitCount = 0;
    private int maxHits = 20;

    private bool isEnemyAttacking = false;

    void Start()
    {
        InvokeRepeating("CheckEnemyAttack", 1.0f, 1.0f);
    }

    void Update()
    {

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireSwordEnemy") || collision.gameObject.CompareTag("IceSwordEnemy"))
        {
            isEnemyAttacking = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireSwordEnemy") || collision.gameObject.CompareTag("IceSwordEnemy"))
        {
            isEnemyAttacking = false;

        }

    }

    void CheckEnemyAttack()
    {
        if (isEnemyAttacking)
        {
            TakeHits();
        }
    }
}