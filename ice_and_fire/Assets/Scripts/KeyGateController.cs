using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGateController : MonoBehaviour
{
    public GameObject hingedGate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openGate() {
        HingeJoint2D hingeJoint = hingedGate.GetComponent<HingeJoint2D>();
        hingeJoint.useMotor = true;

    }


}


