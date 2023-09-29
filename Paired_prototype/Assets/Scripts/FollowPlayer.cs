using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform
    public float smoothSpeed = 0.125f; // Adjust this to control camera follow speed
    public Vector3 offset = new Vector3(0, 0, -10); // Adjust the Z offset for camera depth

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
