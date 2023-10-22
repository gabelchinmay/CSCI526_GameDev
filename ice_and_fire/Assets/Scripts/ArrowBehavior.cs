using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    private float moveSpeed = 5f;
    // Start is called before the first frame update
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
        //PerformActionBeforeDestroy();
        if (!collision.CompareTag("enemy"))
        {
            Destroy(gameObject);

        }
    }
            // Destroy the arrow
            
}
