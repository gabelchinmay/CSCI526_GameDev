using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateIfWrongColor : MonoBehaviour
{   
    //Private Variables
    public GameObject player;
    
    //Private Variables
    private SpriteRenderer sr;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<SpriteRenderer>().color == sr.color || player.GetComponent<SpriteRenderer>().color == Color.white)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (player.GetComponent<SpriteRenderer>().color != sr.color)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
