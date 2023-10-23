using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDragonController : MonoBehaviour
{
    public float changingTime = 5.0f;
    // private Animator playerAnimator;
    public GameObject IceDragon; 
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = IceDragon.GetComponent<Animator>();
        anim.SetBool("flaming",false);
        anim.SetBool("isAlive",true);

        
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
}
