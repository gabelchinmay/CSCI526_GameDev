using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireDragonController : MonoBehaviour
{
    // switching time between idle and flaming
    public float changingTime = (float) (2 * Math.PI - 2);
    public float flamingTime = 2.0f;


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

    // moving
    private Vector3 initialPosition;
    // private Vector3 initialRotation;
    public float amplitude = 5f;
    public float frequency = 1 / 2f;
    private float previousOscillation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
        anim = FireDragon.GetComponent<Animator>();
        anim.SetBool("flaming",false);
        anim.SetBool("isAlive",true);

        healthBar = GetComponentInChildren<FloatingHealthBar>();

        // // assign the Fire component
        // fireTransform = FireDragon.transform.Find("Fire");
        // if (fireTransform != null)
        // {
        //     // Access the 'Fire' GameObject's components here
        //     fire = fireTransform.gameObject;
        //     // You can now access the components of the 'Fire' GameObject
        // }
        // else
        // {
        //     Debug.LogError("Fire GameObject not found under the dragon.");
        // }
        // fire.SetActive(false);

        // initialize moving
        initialPosition = transform.position;
        // initialRotation = transform.rotation;
        previousOscillation = amplitude * Mathf.Sin(frequency * Time.time);

        
    }

    // Update is called once per frame
    void Update()
    {
        // bool isFlaming = anim.GetBool("flaming");
        // bool isHurt = anim.GetBool("isHurt");
        // fire.SetActive(isFlaming);

        // move back and forth
        float oscillation = amplitude * Mathf.Sin(frequency * Time.time);
        transform.position = initialPosition + Vector3.right * oscillation;
            // playerAnimator.SetFloat("speed", oscillation);

        if (oscillation > previousOscillation)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        previousOscillation = oscillation;

        

        // set flaming timer
        changingTime -= Time.deltaTime;
        if (changingTime < 0){
            
            anim.SetBool("flaming",true);
            // fire.SetActive(true);
            
            
        }
        if (changingTime < -flamingTime){
            // fire.SetActive(false);
            anim.SetBool("flaming",false);
            changingTime = (float) (2 * Math.PI - 2);
            
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
    }

    // disappear
    private void Des(){
        Destroy(FireDragon);
    }

    // get hurt by arrows
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "IceArrow"
        if (other.CompareTag("IceArrow"))
        {
            takeDamage(ARROW_DAMAGE);
        }
    }
    
}
