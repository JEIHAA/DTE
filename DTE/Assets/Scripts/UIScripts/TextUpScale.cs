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
        // ũ�Ⱑ �ִ� ũ�⿡ �����ϸ� ��� ���·� ����
        if (defaultTransSize >= maxSize)
        {
            isScalingUp = false;
        }
        // ũ�Ⱑ �ּ� ũ�⿡ �����ϸ� Ȯ�� ���·� ����
        else if (defaultTransSize <= minSize)
        {
            isScalingUp = true;
        }

        // Ȯ�� �Ǵ� ��� ���¿� ���� ũ�� ����
        if (isScalingUp)
        {
            defaultTransSize += transSize * Time.deltaTime;
        }
        else
        {
            defaultTransSize -= transSize * Time.deltaTime;
        }

        // ������Ʈ ũ�� ����
        transform.localScale = new Vector3(defaultTransSize, defaultTransSize, defaultTransSize);
    }
}