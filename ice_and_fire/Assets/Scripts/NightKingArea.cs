using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightKingArea : MonoBehaviour
{
    public Transform enemyToFollow;
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
    void Update()
    {
        if (enemyToFollow != null)
        {
            transform.position = enemyToFollow.position;
        }
    }
}
