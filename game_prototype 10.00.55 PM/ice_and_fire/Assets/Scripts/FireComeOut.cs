using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireComeOut : MonoBehaviour
{

    public Transform firePoint;
    public GameObject firePrefeb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(firePrefeb,firePoint.position,firePoint.rotation);
        
    }
}
