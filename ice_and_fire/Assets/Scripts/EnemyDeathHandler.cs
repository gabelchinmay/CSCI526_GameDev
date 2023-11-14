using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public GameObject DragonEgg;
    public Vector3 targetPosition; 

    void Update()
    {
        if (!gameObject.activeInHierarchy)
        {
            if (DragonEgg != null)
            {
                DragonEgg.transform.position = targetPosition;
            }
        }
    }
}
