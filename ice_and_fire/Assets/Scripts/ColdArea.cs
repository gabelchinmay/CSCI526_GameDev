using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdArea : MonoBehaviour
{
<<<<<<< Updated upstream
    public Transform enemyToFollow;

=======
    public Transform enemyToFollow; 
>>>>>>> Stashed changes
    void Update()
    {
        if (enemyToFollow != null)
        {
            transform.position = enemyToFollow.position;
        }
    }
}
