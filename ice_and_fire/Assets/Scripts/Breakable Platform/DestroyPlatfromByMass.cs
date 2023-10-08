using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyPlatfromByMass : MonoBehaviour
{
    private bool playerOnPlatform = false;
    public int breakMass = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().mass >= breakMass)
        {
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerOnPlatform && collision.gameObject.GetComponent<Rigidbody2D>().mass >= breakMass)
            {
                Destroy(gameObject);
            }

            playerOnPlatform = false;
        }
    }
}
