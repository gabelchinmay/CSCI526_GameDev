using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlink : MonoBehaviour
{
    private SpriteRenderer areaRenderer;  // �����޸�������ɫ�� SpriteRenderer ���
    private Color originalColor;

    public float blinkSpeed = 1.0f;  // ��˸�ٶ�

    void Start()
    {
        // ��ȡ�����������ӵ� GameObject �� SpriteRenderer ���
        areaRenderer = GetComponent<SpriteRenderer>();
        if (areaRenderer == null)
        {
            Destroy(this);
            return;
        }
        originalColor = areaRenderer.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered Trigger");
        if (!other.CompareTag("Player"))
        {
            Debug.Log("Blink");
            // ���� enemy ��������ʱ���� SpriteRenderer ͸��������Ϊ0
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        float lerpTime = 0f;

        while (lerpTime < 1f)
        {
            // ӳ��͸����ֵ�� 0 �� 1 �ķ�Χ
            float targetAlpha = Mathf.Lerp(0f, 0.5f, lerpTime);

            // ������ɫ
            Color lerpedColor = originalColor;
            lerpedColor.a = targetAlpha;
            areaRenderer.color = lerpedColor;

            // �ȴ�һ֡
            yield return null;

            // ���²�ֵʱ��
            lerpTime += Time.deltaTime * blinkSpeed;
        }

        // ����͸����
        areaRenderer.color = originalColor;
    }
}
