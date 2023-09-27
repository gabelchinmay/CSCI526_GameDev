using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gateScript : MonoBehaviour
{
    public EatTarget test;    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D gate)

    {
        test = new EatTarget();
        print(EatTarget.flag);
        if (gate.name == "player")
        {
            if (EatTarget.flag == 3)
            {
                SceneManager.LoadSceneAsync(2);
            }


        }



    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
