using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGateByMass : MonoBehaviour
{
    public float massThreshold = 50f;
    //public GameObject player;
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Player") && (float) collision.gameObject.GetComponent<Rigidbody2D>().mass > massThreshold)
        {
            Destroy(this.gameObject);
        }
    }
}
