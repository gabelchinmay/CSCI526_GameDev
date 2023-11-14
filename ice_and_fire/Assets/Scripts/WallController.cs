using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject associatedEnemy;

    void Update()
    {
        if (associatedEnemy == null)
        {
            Destroy(this.gameObject);
        }
    }
}
