using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFireGateController : MonoBehaviour
{
    public string gateType;
    public GameObject player;
    private BoxCollider2D gateCollider;
    // Start is called before the first frame update
    void Start()
    {
        this.gateCollider = GetComponent<BoxCollider2D>();
        this.gateCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        gateCollider = GetComponent<BoxCollider2D>();

        if(player.GetComponent<PlayerController>().getPlayerStyle() == this.gateType)
        {
            this.gateCollider.enabled = false;

        }
        else
        {
            this.gateCollider.enabled = true;

        }
        

    }
    



}
