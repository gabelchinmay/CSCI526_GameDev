using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatElectron : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        //if collision between player and electron happened
        //make electron merge into player and player change into larger size and larger mass

<<<<<<< HEAD
        if (other.name == "player"){
=======
        if (other.name == "Player"){
>>>>>>> 7496ea6d6ecfa40f44891ed20b42823fdb680644

            Destroy(this.gameObject);
            other.gameObject.transform.localScale += new Vector3(0.2f,0.2f,0.2f);
            other.gameObject.GetComponent<Rigidbody2D>().mass += 1;

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
