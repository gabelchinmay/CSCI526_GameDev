using UnityEngine;

public class TriggerTextReminder : MonoBehaviour
{
    public GameObject targetObject; // ��������Ҫ����/ͣ�õĶ���
    public float triggerDistance = 5.0f; // ��������
    public Transform player; // ��Ҷ��������
    private bool isEnabled = true; // ���ڸ���Ŀ������״̬

    private void OnEnable()
    {
        if (targetObject != null)
        {
            UpdateObjectActivation(); // ��ʼ��ʱ���ݾ�����¶����״̬
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

            UpdateObjectActivation(); // ���ݾ�����¶����״̬
        }
    }

    // ���� isEnabled ���������¶���ļ���״̬
    private void UpdateObjectActivation()
    {
        targetObject.SetActive(isEnabled);
    }
}
