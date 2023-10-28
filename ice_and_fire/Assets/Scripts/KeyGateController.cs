using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGateController : MonoBehaviour
{
    public string style;
    public GameObject hingedGate;
    // Start is called before the first frame update
    void Start()
    {
        
        if (style == "ice")
        {
            openGate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    public void passStyle(string style)
    {
        this.style = style;
    }

    public void openGate() {
        HingeJoint2D hingeJoint = hingedGate.GetComponent<HingeJoint2D>();
        hingeJoint.GetComponent<Collider2D>().enabled = false;

    }

    




}


