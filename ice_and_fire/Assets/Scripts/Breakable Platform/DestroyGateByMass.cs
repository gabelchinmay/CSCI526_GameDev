using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGateByMass : MonoBehaviour
{
    public GameObject player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.GetComponent<Rigidbody2D>().mass > 50)
        {
            Destroy(this.gameObject);
        }
    }
}
