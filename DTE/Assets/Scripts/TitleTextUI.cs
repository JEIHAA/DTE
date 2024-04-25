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
            // 타이틀이 아래로 떨어지는 동안 실행될 코드
            titleRectTransform.anchoredPosition -= new Vector2(0f, speed * Time.deltaTime);

            // 타이틀이 목표 Y 위치에 도달하면 떨어지는 애니메이션을 멈춘다.
            if (titleRectTransform.anchoredPosition.y <= endY)
            {
                isFalling = false;
            }
        }
    }
}
