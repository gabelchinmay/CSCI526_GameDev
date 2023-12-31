using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightKingArea : MonoBehaviour
{
    private int maxHits = 5;
    private int hitCount = 0;
    public Transform enemyToFollow;
    private SendToGoogle sendToGoogle;

    void Start()
    {
        this.sendToGoogle = FindObjectOfType<SendToGoogle>();
    }
    void Update()
    {
        if (enemyToFollow != null)
        {
            transform.position = enemyToFollow.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void TakeHits(int amt)
    {
        // Track hits
        hitCount += amt;

        // Access SendToGoogle to update the hit count
        if (sendToGoogle != null)
        {
            sendToGoogle.ValidSwordAttackCount();
        }

        if (hitCount >= maxHits)
        {
            SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
            if (sendToGoogle != null)
            {
                sendToGoogle.killEnemy();
            }
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireArrow"))
        {
            this.TakeHits(1);
            Destroy(other.gameObject);
        }
    }
}
