using UnityEngine;

public class threeSecTips : MonoBehaviour
{
    public string targetTag = "Player"; // ��������ı�ǩ
    public GameObject textObject; // ������ʾ�ı��Ķ���
    public float displayDelay = 3.0f; // ��ʾ�ı����ӳ�ʱ��
    private bool canDisplayText = false; // �Ƿ������ʾ�ı�
    private float timeSinceCollision = 0.0f; // ������ײ���ʱ��

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
