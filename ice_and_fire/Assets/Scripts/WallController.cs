using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject associatedEnemy;

    void Start()
    {
        if (associatedEnemy != null)
        {
            Debug.LogError("������������WallController��associatedEnemy�ֶΣ�");
        }
    }

    void Update()
    {
        if (associatedEnemy == null)
        {
            Destroy(this.gameObject);
        }
    }
}
