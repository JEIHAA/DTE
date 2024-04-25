using UnityEngine;
using TMPro;

public class TitleTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float startY = 500f;
    [SerializeField] private float endY = 100f;

    private RectTransform titleRectTransform;
    private bool isFalling = false;

    private void Start()
    {
        titleRectTransform = titleText.GetComponent<RectTransform>();
        titleRectTransform.anchoredPosition = new Vector2(0f, startY);
        isFalling = true;
    }

    private void Update()
    {
        if (isFalling)
        {
            // Ÿ��Ʋ�� �Ʒ��� �������� ���� ����� �ڵ�
            titleRectTransform.anchoredPosition -= new Vector2(0f, speed * Time.deltaTime);

            // Ÿ��Ʋ�� ��ǥ Y ��ġ�� �����ϸ� �������� �ִϸ��̼��� �����.
            if (titleRectTransform.anchoredPosition.y <= endY)
            {
                isFalling = false;
            }
        }
    }
}
