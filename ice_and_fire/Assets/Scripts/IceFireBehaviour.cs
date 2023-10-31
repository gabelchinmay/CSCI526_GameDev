using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFireBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnIceDragonFireEnter(this);
            }
        }

        if (other.gameObject.CompareTag("FireArrowEnemy") || other.gameObject.CompareTag("FireSwordEnemy"))
        {
            // Debug.Log("IceDragonfire on enemy!");
            SwordEnemyBehaviour swordEnemyController = other.gameObject.GetComponent<SwordEnemyBehaviour>();
            ArrowEnemyBehavior arrowEnemyController = other.gameObject.GetComponent<ArrowEnemyBehavior>();
            if (swordEnemyController != null)
            {
                swordEnemyController.TakeHits(2);
            }

            if (arrowEnemyController != null)
            {
                arrowEnemyController.TakeHits(2);
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
                playerController.OnIceDragonFireExit(this);
            }
        }
    }
}
