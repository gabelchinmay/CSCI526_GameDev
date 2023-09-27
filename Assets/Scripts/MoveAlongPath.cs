using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    //Private Variables
    private int currIndex = 0;
    
    //Public Variables
    public Vector2[] setPoints;
    public float movingSpeed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, setPoints[currIndex], movingSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, setPoints[currIndex]) < 0.02f)
        {
            currIndex++;
            if (currIndex >= setPoints.Length)
            {
                currIndex = 0;
            }
        }
    }
}
