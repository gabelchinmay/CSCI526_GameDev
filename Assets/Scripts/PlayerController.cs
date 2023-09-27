using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    //Private variables
    private float horizaontalInput;
    private float movingSpeed = 5.0f;
    private float jumpSpeed = 10.0f;
    private float speedUpFalling = 10.0f;

    private bool isGrounded;

    private Rigidbody2D rb;

    public GameObject player;// added

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Get Player Input
        horizaontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * movingSpeed * horizaontalInput);
        
        if(Input.GetKeyDown("space") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown("down"))
        {
            rb.gravityScale = speedUpFalling;
        }
    }
    // If double jump is not allowed
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

 

    void OnBecameInvisible()
    {
        player = GameObject.Find("Player");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
