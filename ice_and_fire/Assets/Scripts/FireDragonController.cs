using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragonController : MonoBehaviour
{
    // switching time between idle and flaming
    public float changingTime = 5.0f;

    // gameobject and animator
    public GameObject FireDragon; 
    private Animator anim;

    // time delay for events like return to idle and disappear
    private float DELAY = 1;
    private float ARROW_DAMAGE = 100;

    // initialize the health bar
    [SerializeField] float health, maxHealth = 1000f;
    [SerializeField] FloatingHealthBar healthBar;

    // flaming
    Transform fireTransform;
    GameObject fire;


    // Start is called before the first frame update
    void Start()
    {
        
        anim = FireDragon.GetComponent<Animator>();
        anim.SetBool("flaming",false);
        anim.SetBool("isAlive",true);

        healthBar = GetComponentInChildren<FloatingHealthBar>();

        // assign the Fire component
        fireTransform = FireDragon.transform.Find("Fire");
        if (fireTransform != null)
        {
            // Access the 'Fire' GameObject's components here
            fire = fireTransform.gameObject;
            // You can now access the components of the 'Fire' GameObject
        }
        else
        {
            Debug.LogError("Fire GameObject not found under the dragon.");
        }
        fire.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        bool isFlaming = anim.GetBool("flaming");
        fire.SetActive(isFlaming);

        // Set the visibility of the head GameObject
        

        changingTime -= Time.deltaTime;
        if (changingTime < 0){
            
            anim.SetBool("flaming",true);
            // fire.SetActive(true);
            
            
        }
        if (changingTime < -3.0f){
            // fire.SetActive(false);
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
            Invoke("Des", DELAY);

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
    private void Des(){
        Destroy(FireDragon);
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
