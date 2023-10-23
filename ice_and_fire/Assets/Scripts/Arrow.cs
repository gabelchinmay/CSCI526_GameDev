using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private SendToGoogle sendToGoogle;

    private void Start()
    {
        sendToGoogle = SendToGoogle.currentSendToGoogle; // 获取 SendToGoogle 类的实例
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // 假设敌人的标签是 "Enemy"
        {
            sendToGoogle.ArrowHitsEnemyCount++; // 增加射中敌人的箭矢数量
        }
    }
}

