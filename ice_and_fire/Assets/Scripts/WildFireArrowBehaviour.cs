using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildFireArrowBehaviour : MonoBehaviour
{
    private float moveSpeed = 5f;
    public GameObject fireAreaPrefab;

    void Start()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform_Normal"))
        {

            Instantiate(fireAreaPrefab, transform.position + Vector3.down * 0.2f, Quaternion.identity);


            // Destroy the arrow
            Destroy(gameObject);
            Debug.Log("Wildfire destroyed!");
        }
    }


}

