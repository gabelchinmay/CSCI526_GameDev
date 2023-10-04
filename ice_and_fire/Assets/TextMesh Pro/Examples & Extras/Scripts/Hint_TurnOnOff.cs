using UnityEngine;

public class Hint_TurnOnOff : MonoBehaviour
{
    public GameObject targetObject; // This is the object you want to activate/deactivate
    public KeyCode enableKey = KeyCode.Alpha9; // Set to the number 9 key
    public KeyCode disableKey = KeyCode.Alpha0; // Set to the number 0 key
    private bool isEnabled = true; // Used to track the state of the target object

    // Use the OnEnable method to ensure that the target object's activation status is set when the script is enabled
    private void OnEnable()
    {
        // Check if the target object is not null
        if (targetObject != null)
        {
            // Set the target object's activation status based on the isEnabled variable
            targetObject.SetActive(isEnabled);
        }
    }

    public void Update()
    {
        // Check if the specified keys are pressed
        if (Input.GetKeyDown(enableKey))
        {
            // If the target object is not null
            // Toggle the activation status of the target object
            isEnabled = true; // Update the isEnabled variable
            targetObject.SetActive(true);
        }

        if (Input.GetKeyDown(disableKey))
        {
            // Toggle the activation status of the target object
            isEnabled = false; // Update the isEnabled variable
            targetObject.SetActive(false);
        }
    }
}
