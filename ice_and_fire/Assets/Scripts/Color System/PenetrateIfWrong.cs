using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateIfWrong : MonoBehaviour
{
    //Private Variables
    public GameObject player;
    
    //Private Variables
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(sr.color);
        //Debug.Log(player.GetComponent<SpriteRenderer>().color);
        if (player.GetComponent<SpriteRenderer>().color == sr.color)
        {
            bc.enabled = true;
        }
        else
        {
            bc.enabled = false;
        }
    }
}
