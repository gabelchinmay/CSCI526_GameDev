using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatformByTime : MonoBehaviour
{
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
        Destroy(this.gameObject, 2);
    }
}
