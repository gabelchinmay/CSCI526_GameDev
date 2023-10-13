using UnityEngine;

public class TriggerTextReminder : MonoBehaviour
{
    public GameObject targetObject; // 这是你想要激活/停用的对象
    public float triggerDistance = 5.0f; // 触发距离
    public Transform player; // 玩家对象的引用
    private bool isEnabled = true; // 用于跟踪目标对象的状态

    private void OnEnable()
    {
        if (targetObject != null)
        {
            UpdateObjectActivation(); // 初始化时根据距离更新对象的状态
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

            UpdateObjectActivation(); // 根据距离更新对象的状态
        }
    }

    // 根据 isEnabled 变量来更新对象的激活状态
    private void UpdateObjectActivation()
    {
        targetObject.SetActive(isEnabled);
    }
}
