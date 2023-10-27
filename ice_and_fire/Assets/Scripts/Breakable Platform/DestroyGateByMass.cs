using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGateByMass : MonoBehaviour
{
    private float massThreshold = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (float) collision.gameObject.GetComponent<Rigidbody2D>().mass > massThreshold)
        {
            Destroy(this.gameObject);
        }
    }
}
