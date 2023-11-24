using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlink : MonoBehaviour
{
    private SpriteRenderer areaRenderer;  // 用于修改区域颜色的 SpriteRenderer 组件
    private Color originalColor;

    public float blinkSpeed = 1.0f;  // 闪烁速度

    void Start()
    {
        // 获取触发器所附加的 GameObject 的 SpriteRenderer 组件
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
            // 当有 enemy 进入区域时，将 SpriteRenderer 透明度设置为0
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        float lerpTime = 0f;

        while (lerpTime < 1f)
        {
            // 映射透明度值到 0 到 1 的范围
            float targetAlpha = Mathf.Lerp(0f, 0.5f, lerpTime);

            // 设置颜色
            Color lerpedColor = originalColor;
            lerpedColor.a = targetAlpha;
            areaRenderer.color = lerpedColor;

            // 等待一帧
            yield return null;

            // 更新插值时间
            lerpTime += Time.deltaTime * blinkSpeed;
        }

        // 重置透明度
        areaRenderer.color = originalColor;
    }
}
