using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowEnemyBehavior : MonoBehaviour
{
    public GameObject arrowPrefab;
    private float shootInterval = 1.5f;
    private int hitCount = 0;
    private int maxHits = 3;

    void Start()
    {
        InvokeRepeating("ShootArrow", 0f, shootInterval);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootArrow()
    {
        Vector3 offset = transform.position + Vector3.up * 1.5f + Vector3.left * 2f;
        GameObject arrow = Instantiate(arrowPrefab, offset, Quaternion.identity);
        Rigidbody2D a = arrow.GetComponent<Rigidbody2D>();
        a.velocity = new Vector2(-35f, 0); // Shooting to the left
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "arrow"
        if (other.CompareTag("arrow"))
        {
            // Increase the hit count
            hitCount++;

            // Destroy the arrow that hit the enemy
            Destroy(other.gameObject);

            // Check if hit count reached max hits
            if (hitCount >= maxHits)
            {
                Destroy(gameObject);
            }
        }
    }

}
