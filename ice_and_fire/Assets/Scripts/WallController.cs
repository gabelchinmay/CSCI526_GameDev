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
            Debug.LogError("请关联怪物对象到WallController的associatedEnemy字段！");
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
