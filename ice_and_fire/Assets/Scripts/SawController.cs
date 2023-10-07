using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 45f; // Adjust the rotation speed as needed.
    public float amplitude = 5f; // Adjust the amplitude of oscillation.
    public float frequency = 1/2f; // Adjust the frequency of oscillation.


    private Vector3 initialPosition;


    void Start()
    {
        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(-1*Vector3.forward * rotationSpeed * Time.deltaTime);
        float oscillation = amplitude * Mathf.Sin(frequency * Time.time);
        //Debug.Log("Oscillation: " + oscillation);
        // Set the new position of the sprite based on the oscillation
        transform.position = initialPosition + Vector3.right * oscillation;
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnSawEnter(this);
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
                playerController.OnSawExit(this);
            }
        }
    }
}
