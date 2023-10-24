using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    public GameObject fire; // Assign the Fire GameObject in the Inspector
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // Check if the IsFlaming parameter is true in the Animator
        bool isFlaming = animator.GetBool("flaming");

        // Set the visibility of the head GameObject
        fire.SetActive(isFlaming); // Show the Fire if IsFlaming is true, hide it otherwise
        
    }
}
