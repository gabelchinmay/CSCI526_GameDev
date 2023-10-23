using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragonController : MonoBehaviour
{
    public float changingTime = 5.0f;
    // private Animator playerAnimator;
    public GameObject FireDragon; 
    private Animator anim;

    [SerializeField] float health, maxHealth = 1000f;
    [SerializeField] FloatingHealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        
        anim = FireDragon.GetComponent<Animator>();
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
        // if(changingTime <= 2){
        //     takeDamage(1);
        // }
        

    }

    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        anim.SetBool("isHurt", true);
        if (health <= 0)
        {
            anim.SetBool("isHurt", false);
            anim.SetBool("isAlive",false);
            // yield return new WaitForSeconds(2);
            Invoke("Des", 1);

        }
    }

    void Des(){
        Destroy(FireDragon);
    }
       
}
