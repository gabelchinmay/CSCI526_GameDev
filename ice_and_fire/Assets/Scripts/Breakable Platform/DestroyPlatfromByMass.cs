using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyPlatfromByMass : MonoBehaviour
{

    public GameObject player;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (player.GetComponent<Rigidbody2D>().mass > 10)
        {
            Destroy(this.gameObject);
        }
    }
}
