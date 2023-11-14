using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearEnemyBehavior : MonoBehaviour
{
    public int maxHits = 6;
    
    //Initialize Healthbar related component
    [SerializeField] float health, maxHealth = 6.0f;
    [SerializeField] FloatingHealthBar healthBar;

    private string spearEnemyType;
    private int hitCount = 0;
    private bool isOnFire = false;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        this.spearEnemyType = this.gameObject.tag;
        this.healthBar = GetComponentInChildren<FloatingHealthBar>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeHits(int amt)
    {
        hitCount +=amt;
        health -= (float)amt;
        
        healthBar.UpdateHealthBar(health,maxHealth);
        StartCoroutine(resetHurtAnimation());
        
        
        if (hitCount >= maxHits)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireArrow") || other.CompareTag("IceArrow"))
        {
            Destroy(other.gameObject);
            
        }


        if (spearEnemyType == "FireSpear")
        {
            if (other.CompareTag("IceArrow"))
            {
                this.TakeHits(1);
                Debug.Log("Hit by ice arrow!");
                
            }

        }

        if (other.CompareTag("FireArea"))
        {
            isOnFire = true;
            Debug.Log("On Fire!!!");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FireArea"))
        {
            isOnFire = false;
            Debug.Log("Off Fire!!!");

        }

    }


    private IEnumerator resetHurtAnimation()
    {
        yield return new WaitForSeconds(1f);
    }


    private IEnumerator InflictDamages()
    {
        while (true)
        {
            if (isOnFire)
            {
                this.TakeHits(1);
                Debug.Log("Spear Enemy damaged");
            }
            yield return new WaitForSeconds(1.0f);

        }
    }
}
