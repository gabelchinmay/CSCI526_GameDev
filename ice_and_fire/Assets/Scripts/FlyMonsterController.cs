using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonsterController : MonoBehaviour
{
    public float amplitude = 5f; // Adjust the amplitude of oscillation.
    public float frequency = 1 / 2f; // Adjust the frequency of oscillation.
 

    private void Start()
    {
        float oscillation = amplitude * Mathf.Sin(frequency * Time.time);
    }

    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnMonsterEnter(this);
            }
        }
    }



    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnMonsterExit(this);
            }
        }
    }

 


}



