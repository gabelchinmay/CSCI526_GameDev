using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;


    void Start()
    {
        //fire movements
        rb.velocity = transform.right * speed;
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        Destroy(gameObject);
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
