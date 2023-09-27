using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatformByJump : MonoBehaviour
{
    private int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (count == 3)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        count++;
    }
}
