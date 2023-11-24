using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetIndicator : MonoBehaviour
{
    public Transform[] Targets;
    public float HideDistance;
    private int currentTargetIndex = 0;

    void Start()
    {
        if (Targets == null || Targets.Length == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Targets == null || Targets.Length == 0)
        {
            Destroy(this.gameObject);
            return;
        }

        if (Targets[currentTargetIndex] == null)
        {
            currentTargetIndex = (currentTargetIndex + 1) % Targets.Length;
            if (currentTargetIndex == 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            var dir = Targets[currentTargetIndex].position - transform.position;

            if (dir.magnitude < HideDistance)
            {
                SetChildObjectsActive(false);
            }
            else
            {
                SetChildObjectsActive(true);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    void SetChildObjectsActive(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
