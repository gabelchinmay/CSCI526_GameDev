using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject hingedGate;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        Vector2 checkPosition = transform.position;

        Collider2D  collider = CheckOverlapWithTolerance(checkPosition, 0.15f);
        if(collider != null)
        {
            if(collider.tag == "Placeholder") {
                HingeJoint2D hingeJoint = hingedGate.GetComponent<HingeJoint2D>();
                hingeJoint.useMotor = true;
                spriteRenderer.enabled = false;

            }

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HingeJoint2D hingeJoint = hingedGate.GetComponent<HingeJoint2D>();
            hingeJoint.useMotor = true; 
            spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {

            HingeJoint2D hingeJoint = hingedGate.GetComponent<HingeJoint2D>();
            hingeJoint.useMotor = false; 
            spriteRenderer.enabled = true;

        }
    }

    private Collider2D CheckOverlapWithTolerance(Vector2 position, float tolerance)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, new Vector2(tolerance, tolerance), 0);

        foreach (Collider2D collider in colliders)
        {
            if (collider != null && collider.tag == "Placeholder")
            {
                return collider;
            }
        }

        return null;
    }


}
