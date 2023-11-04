using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFireGateController : MonoBehaviour
{
    public string gateType;
    public GameObject player;
    private BoxCollider2D gateCollider;
    private int collisionCount = 0; // ���ڸ�����ײ�����ļ�����
    private SendToGoogle sendToGoogle;

    void Start()
    {
        this.gateCollider = GetComponent<BoxCollider2D>();
        this.gateCollider.enabled = true;
        this.sendToGoogle = FindObjectOfType<SendToGoogle>();
    }

    void Update()
    {
        gateCollider = GetComponent<BoxCollider2D>();

        if (player.GetComponent<PlayerController>().getPlayerStyle() == this.gateType)
        {
            this.gateCollider.enabled = false;
        }
        else
        {
            this.gateCollider.enabled = true;
        }
    }

    // ����ײ����ʱ������
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �����ײ�������Ƿ���"FireGate"��ǩ
        {
            // ����ײ����ʱ���Ӽ�����
            collisionCount++;
            if (sendToGoogle != null)
            {
                sendToGoogle.hitWrongGate();

            }
            // ���������Ϣ
            Debug.Log("Collision Count: " + collisionCount);
        }
    }
}
