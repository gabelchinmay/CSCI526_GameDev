using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowEnemyBehavior : MonoBehaviour
{
    public GameObject arrowPrefab;
    private float shootInterval = 1f;
    private int hitCount = 0;
    private int maxHits = 3;
    private Animator playerAnimator;

    private bool isOnFire = false;


    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("shoot", true);
        InvokeRepeating("ShootArrow", 0f, shootInterval);
        StartCoroutine(InflictDamages());

    }

    void Update()
    {
        
    }

    void ShootArrow()
    {
        Vector3 offset = transform.position + Vector3.up * 1f + Vector3.left * 2f;
        GameObject arrow = Instantiate(arrowPrefab, offset, Quaternion.identity);
        Rigidbody2D a = arrow.GetComponent<Rigidbody2D>();
        a.velocity = new Vector2(-15f, 0); // Shooting to the left
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "arrow"
        if (other.CompareTag("arrow"))
        {
            hitCount++;

            Destroy(other.gameObject);

            if (hitCount >= maxHits)
            {
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("FireArea"))
        {
            isOnFire = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FireArea"))
        {
            isOnFire = false;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnArrowEnemyEnter(this);
            }

        }


    }


    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnArrowEnemyExit(this);
            }
        }
    }

    public void TakeHits(int amt)
    {
        hitCount+=amt;

        if (hitCount >= maxHits)
        {
            Destroy(gameObject);
        }
    }



    private IEnumerator InflictDamages()
    {
        while (true)
        {
            if (isOnFire)
            {
                this.TakeHits(1);
                Debug.Log("Arrow Enemy damaged");
            }
            yield return new WaitForSeconds(1.0f);

        }
    }

}
