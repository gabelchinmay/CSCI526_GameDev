using UnityEngine;

public class PickupItem : MonoBehaviour
{

    public string Collect()
    {
        string itemType = gameObject.tag;
        switch (itemType)
        {
            case "HealthUp":

                return "Health Up Collected";

            case "SpeedUp":
                return "Speed Up Collected";

            case "Defrost":
                return "De-frost Collected";

            default:
                return "Unknown Item Collected";
        }
    }
}
