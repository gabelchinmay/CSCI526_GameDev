using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdArea : MonoBehaviour
{
    private int maxHits = 3;
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
    }
    public void TakeHits(int amt)
    {
        // 这部分如何统计
        hitCount += amt;
        if (sendToGoogle != null)
        {
            sendToGoogle.SwordAttackCount(hitCount);
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
