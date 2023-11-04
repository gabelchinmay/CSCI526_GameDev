using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFireGateController : MonoBehaviour
{
    public string gateType;
    public GameObject player;
    private BoxCollider2D gateCollider;
    private int collisionCount = 0; // 用于跟踪碰撞次数的计数器
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

    // 当碰撞发生时被调用
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 检查碰撞的物体是否有"FireGate"标签
        {
            // 在碰撞发生时增加计数器
            collisionCount++;
            if (sendToGoogle != null)
            {
                sendToGoogle.hitWrongGate();

            }
            // 输出调试信息
            Debug.Log("Collision Count: " + collisionCount);
        }
    }
}
