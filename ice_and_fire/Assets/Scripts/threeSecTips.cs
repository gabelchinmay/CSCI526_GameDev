using UnityEngine;

public class ThreeSecTips : MonoBehaviour
{
    public string targetTag = "Player"; // 触发对象的标签
    public GameObject textObject; // 用于显示文本的对象
    public float displayDelay = 3.0f; // 显示文本的延迟时间
    public bool canDisplayText = false; // 是否可以显示文本
    public float timeSinceCollision = 0.0f; // 跟踪碰撞后的时间

    private void Update()
    {
        if (canDisplayText && Input.GetKeyDown(KeyCode.F))
        {
            textObject.SetActive(false);
            canDisplayText = false;
        }

        timeSinceCollision += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (timeSinceCollision >= displayDelay)
            {
                textObject.SetActive(true);
                canDisplayText = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            textObject.SetActive(false);
            canDisplayText = false;
            timeSinceCollision = 0.0f;
        }
    }
}
