using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EatTarget : MonoBehaviour
{
    public static float flag = 0f;
    public string gameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //if collision between player and electron happened
            //make electron merge into player and player change into larger size

        if (other.name == "player"){

            Destroy(this.gameObject);
            other.gameObject.transform.localScale += new Vector3(0.2f,0.2f,0.2f);
            flag += 1;

        }
        print(flag);


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
