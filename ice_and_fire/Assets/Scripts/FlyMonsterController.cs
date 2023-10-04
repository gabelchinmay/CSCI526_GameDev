using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonsterController : MonoBehaviour
{
    private Vector3 startPoint = new Vector3(0f, 0f, 0f);
    private Vector3 endPoint = new Vector3(5f, 0f, 0f);
    private float speed = 2.0f;

    private bool movingToEnd = true;

    void Update()
    {
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

            if (transform.position == endPoint)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint, speed * Time.deltaTime);

            if (transform.position == startPoint)
            {
                movingToEnd = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnMonsterEnter(this);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnMonsterExit(this);
            }
        }
    }
}
