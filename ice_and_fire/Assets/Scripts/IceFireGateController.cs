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

   
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // "FireGate"
        {
            // 
            collisionCount++;
            if (sendToGoogle != null)
            {
                sendToGoogle.hitWrongGate();

            }
            // 
            Debug.Log("Collision Count: " + collisionCount);
        }
    }
}
