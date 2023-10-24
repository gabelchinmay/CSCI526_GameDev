using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightKingArea : MonoBehaviour
{
    public Transform enemyToFollow;
    void Update()
    {
        if (enemyToFollow != null)
        {
            transform.position = enemyToFollow.position;
        }
    }
}
