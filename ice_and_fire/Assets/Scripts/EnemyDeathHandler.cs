using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public GameObject associatedEnemy;
    public Vector3 targetPosition; 

    void Update()
    {
        if (associatedEnemy == null)
        {
            if (this.gameObject != null)
            {
                this.gameObject.transform.position = targetPosition;
            }
        }
    }
}
