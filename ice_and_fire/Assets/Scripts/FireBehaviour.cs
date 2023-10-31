using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    // public GameObject fire; // Assign the Fire GameObject in the Inspector
    // private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // animator = GetComponent<Animator>();
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
                playerController.OnDragonFireEnter(this);
            }
        }

        if (other.gameObject.CompareTag("IceArrowEnemy") || other.gameObject.CompareTag("IceSwordEnemy") || other.gameObject.CompareTag("Dead") || other.gameObject.CompareTag("WhiteWalker") || other.gameObject.CompareTag("NightKing"))
        {
            
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
                playerController.OnDragonFireExit(this);
            }
        }


    }
}
