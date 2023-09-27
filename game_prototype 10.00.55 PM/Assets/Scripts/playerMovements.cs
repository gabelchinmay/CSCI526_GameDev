using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovements : MonoBehaviour
{
    public float forwardSpeed;
    public float upSpeed;
    public float repulsionForce;
    private Rigidbody2D rb;
    public float reloadBoundary = -10f;
    public float directX, directY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

   
    void Update()
    {
        transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * forwardSpeed * Time.deltaTime);
        transform.Translate(Vector2.up * Input.GetAxis("Vertical") * upSpeed * Time.deltaTime);

        //directX = Input.GetAxis("Horizontal") * forwardSpeed;
        //directY = Input.GetAxis("Vertical") * upSpeed;


        if (transform.position.y < reloadBoundary)
        {
            ReloadScene();
        }
    }

    //void FixedUpdate()
    //{
    //    rb.velocity = new Vector2(directX, directY);
    //}
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == gameObject) // Check if it's the ball that exited the boundary
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Get the build index of the current scene
        SceneManager.LoadScene(currentSceneIndex); // Reloads the current scene
    }
    private void OnCollisionEnter2D(Collision2D collision)

    { 
        if (collision.gameObject.CompareTag("Platform"))
        {

            Vector2 direct = Vector2.up;
            rb.AddForce(direct * repulsionForce, ForceMode2D.Impulse);

            Debug.Log(rb.velocity);
        }
  

        else if (collision.gameObject.CompareTag("Vertical Positive"))
        {
            Vector2 direct = Vector2.left;
            rb.AddForce(direct * repulsionForce, ForceMode2D.Impulse);

            Debug.Log(rb.velocity);
        }
    }


}