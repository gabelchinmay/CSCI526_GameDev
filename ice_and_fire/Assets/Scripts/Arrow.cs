using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private SendToGoogle sendToGoogle;

    private void Start()
    {
        sendToGoogle = SendToGoogle.currentSendToGoogle; // ��ȡ SendToGoogle ���ʵ��
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // ������˵ı�ǩ�� "Enemy"
        {
            sendToGoogle.ArrowHitsEnemyCount++; // �������е��˵ļ�ʸ����
        }
    }
}

