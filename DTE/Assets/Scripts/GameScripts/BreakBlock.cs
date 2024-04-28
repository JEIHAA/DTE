using System.Collections;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    public Sprite newSprite; // 깨진 블록의 스프라이트
    public Sprite oldSprite; // 원래 블록의 스프라이트
    [HideInInspector] public SpriteRenderer spriteRenderer;

    // 시작 위치와 회전값
    private Vector3 startPosition;
    private Quaternion startRotation;

    // 쉐이크 설정
    [SerializeField] private float shakeAmount = 0.1f; // 흔들림 강도
    public float rotationAmount = 0.1f; // 회전량
    public float dropDistance = 30f; // 떨어질 거리
    public float duration = 1f; // 떨어지는 시간
    public float dropSpeedR = 10f; // 떨어지는 시간
    public float timer = 0f; // 시간

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 현재 타일의 원래 스프라이트를 저장
        oldSprite = spriteRenderer.sprite;

        // 초기 위치와 회전값 저장
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌");
            StartCoroutine(StartBrokenBlock());
        }
    }

    private IEnumerator StartBrokenBlock()
    {
        // 지정된 시간 동안 블록을 흔들기
        while (timer < duration)
        {
            timer += Time.deltaTime;
            // 좌우 흔들기
            transform.position = startPosition + new Vector3(Mathf.Sin(timer * 50) * shakeAmount, 0f, 0f);
            // 회전
            transform.rotation = startRotation * Quaternion.Euler(0f, 0f, Mathf.Sin(timer * 50) * rotationAmount);

            yield return null;
        }

        // 아래로 떨어지기
        Vector3 dropPosition = startPosition - new Vector3(0f, dropDistance, 0f);
        float dropSpeed = dropDistance / dropSpeedR; // 떨어지는데 걸리는 시간
        float dropTimer = 0f;

        while (dropTimer < dropSpeed)
        {
            dropTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, dropPosition, dropTimer / dropSpeed);
            yield return null;
        }

        // 블록 파괴
        Destroy(gameObject);
    }

    public void ChangeSpeed(float _value)
    {
        dropSpeedR = _value;
    }
}
