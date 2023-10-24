using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    private float moveSpeed = 5f;
    // Start is called before the first frame update
    // Start is called before the first frame update
    private SendToGoogle sendToGoogle;
    int count = 0;
    void Start()
    {

        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        sendToGoogle = SendToGoogle.currentSendToGoogle;
        //PerformActionBeforeDestroy();
        if (!(collision.CompareTag("sword_enemy") || collision.CompareTag("arrow_enemy")))
        {
            
            Destroy(gameObject);

        }
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("got shoot!");

            sendToGoogle.HitCount();
        }
    }
            // Destroy the arrow
            
}
