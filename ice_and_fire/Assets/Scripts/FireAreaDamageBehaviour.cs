using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAreaDamageBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Find all colliders in contact with this GameObject's Collider2D
        Collider2D[] colliders = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        int count = Physics2D.OverlapCollider(GetComponent<Collider2D>(), filter, colliders);

        // Iterate through the colliders and call TakeHits() on enemies
        for (int i = 0; i < count; i++)
        {
            Collider2D collider = colliders[i];

            // Check if the collider has the "enemy" tag
            if (collider.CompareTag("sword_enemy"))
            {
                // Check if the collider has a component with a TakeHits() method
                SwordEnemyBehaviour enemyController = collider.GetComponent<SwordEnemyBehaviour>();
                if (enemyController != null)
                {
                    // Call TakeHits() on the enemy
                    Debug.Log("Sword Enemy Taking Hits");
                    enemyController.TakeHits(2);
                }
            }

            if (collider.CompareTag("arrow_enemy"))
            {
                ArrowEnemyBehavior enemyController = collider.GetComponent<ArrowEnemyBehavior>();
                if (enemyController != null)
                {
                    Debug.Log("Arrow Enemy Taking Hits");
                    enemyController.TakeHits(2);
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
