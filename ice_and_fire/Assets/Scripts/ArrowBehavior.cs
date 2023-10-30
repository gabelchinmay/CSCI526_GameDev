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
        //if (!(collision.CompareTag("IceSwordEnemy") || collision.CompareTag("IceArrowEnemy") || collision.CompareTag("FireSwordEnemy") || collision.CompareTag("FireArrowEnemy")))
        //{
            
          //  Destroy(gameObject);

        //}

        //TODO: Need to change this for analytics
        if (collision.CompareTag("IceSwordEnemy") || collision.CompareTag("IceArrowEnemy") || collision.CompareTag("FireSwordEnemy") || collision.CompareTag("FireArrowEnemy") || collision.CompareTag("Dead") || collision.CompareTag("WhiteWalker") || collision.CompareTag("NightKing"))
        {
            Debug.Log("got shoot!");
            if (sendToGoogle != null)
            {
                sendToGoogle.HitCount();

            }

        } 
        else
        {
            Destroy(gameObject);
        }
    }
            
}
