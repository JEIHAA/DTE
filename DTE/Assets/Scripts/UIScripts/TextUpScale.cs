using UnityEngine;

public class TextUpScale : MonoBehaviour
{
    private float maxSize = 1.1f;
    private float minSize = 0.9f;
    private float defaultTransSize = 1f;
    private float transSize = 0.5f;
    private bool isScalingUp = true;

    public void TextScaleUpDown()
    {
        // 크기가 최대 크기에 도달하면 축소 상태로 변경
        if (defaultTransSize >= maxSize)
        {
            isScalingUp = false;
        }
        // 크기가 최소 크기에 도달하면 확대 상태로 변경
        else if (defaultTransSize <= minSize)
        {
            isScalingUp = true;
        }

        // 확대 또는 축소 상태에 따라 크기 변경
        if (isScalingUp)
        {
            defaultTransSize += transSize * Time.deltaTime;
        }
        else
        {
            defaultTransSize -= transSize * Time.deltaTime;
        }

        // 오브젝트 크기 변경
        transform.localScale = new Vector3(defaultTransSize, defaultTransSize, defaultTransSize);
    }
}