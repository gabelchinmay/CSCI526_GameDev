using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAlongPath : MonoBehaviour
{
    //Private Variables
    private int currIndex = 0;
    
    //Public Variables
    public Vector2[] setPoints;
    public float movingSpeed = 1.0f;

    // private Vector2 playerOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, setPoints[currIndex]) < 0.02f)
        {
            currIndex++;
            if (currIndex >= setPoints.Length)
            {
                currIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, setPoints[currIndex], movingSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
           collision.gameObject.transform.SetParent(transform);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // save player's position
            Vector3 playerPosition = collision.transform.position;

            collision.transform.SetParent(null);

            // set player's position as the previous saved position
            collision.transform.position = playerPosition;
        }
    }
}
