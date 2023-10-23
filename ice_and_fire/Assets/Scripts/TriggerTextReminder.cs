using UnityEngine;

public class TriggerTextReminder : MonoBehaviour
{
    public GameObject targetObject; 
    public float triggerDistance = 5.0f; 
    public Transform player; 
    private bool isEnabled = true; 

    private void OnEnable()
    {
        if (targetObject != null)
        {
            UpdateObjectActivation(); 
        }
    }

    private void Update()
    {
        if (targetObject != null && player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.position, targetObject.transform.position);

            if (distanceToPlayer <= triggerDistance)
            {
                isEnabled = true;
            }
            else
            {
                isEnabled = false;
            }

            UpdateObjectActivation(); 
        }
    }

    
    private void UpdateObjectActivation()
    {
        targetObject.SetActive(isEnabled);
    }
}
