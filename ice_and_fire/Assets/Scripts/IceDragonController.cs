using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDragonController : MonoBehaviour
{
    // switching time between idle and flaming
    public float changingTime = 5.0f;

    // gameobject and animator
    public GameObject IceDragon; 
    private Animator anim;

    // time delay for events like return to idle and disappear
    private float DELAY = 1;
    private float ARROW_DAMAGE = 100;

    // initialize the health bar
    [SerializeField] float health, maxHealth = 1000f;
    [SerializeField] FloatingHealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        anim = IceDragon.GetComponent<Animator>();
        anim.SetBool("flaming",false);
        anim.SetBool("isAlive",true);

        healthBar = GetComponentInChildren<FloatingHealthBar>();

        
    }

    // Update is called once per frame
    void Update()
    {
        changingTime -= Time.deltaTime;
        if (changingTime < 0){
            anim.SetBool("flaming",true);
            
        }
        if (changingTime < -3.0f){
            anim.SetBool("flaming",false);
            changingTime = 5.0f;
        }
        
    }

    // when being hurt
    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        anim.SetBool("isHurt", true);
        if (health <= 0)
        {
            anim.SetBool("isHurt", false);
            anim.SetBool("isAlive",false);
            Invoke("Des", 1);

        }
        Invoke("closeHurt", (float)(DELAY/4));
    }

    // return to idle
    private void closeHurt()
    {
        anim.SetBool("isHurt", false);
        anim.SetBool("returnIdle", true);
    }

    // disappear
    void Des(){
        Destroy(IceDragon);
    }

    // get hurt by arrows
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "arrow"
        if (other.CompareTag("arrow"))
        {
            takeDamage(ARROW_DAMAGE);

        }

    }
       
}
