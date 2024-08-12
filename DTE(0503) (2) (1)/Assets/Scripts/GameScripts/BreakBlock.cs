using System.Collections;
using UnityEngine;

public class BreakBlock : InteractiveObject
{
    public Sprite coldSprite; // 깨진 블록의 스프라이트
    public Sprite hotSprite; // 원래 블록의 스프라이트

    [SerializeField] DropThron dropThron = null;

    // 시작 위치와 회전값
    private Vector3 startPosition;
    private Quaternion startRotation;

    // 쉐이크 설정
    [SerializeField] private float shakeAmount = 0.1f; // 흔들림 강도
    public float rotationAmount = 0.1f; // 회전량
    public float dropDistance = 5f; // 떨어질 거리
    public float duration = 1f; // 떨어지는 시간
    public float dropSpeedR = 10f; // 떨어지는 시간
    public float timer = 0f; // 시간

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


        // 현재 타일의 원래 스프라이트를 저장
        SpriteRenderer.sprite = hotSprite;

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
        if (dropThron != null)
            dropThron.MovingDrop();
        while (timer < duration)
        {
            timer += Time.deltaTime;
            // 좌우 흔들기
            transform.position = startPosition + new Vector3(Mathf.Sin(timer * 50) * shakeAmount, 0f, 0f);
            // 회전
            transform.rotation = startRotation * Quaternion.Euler(0f, 0f, Mathf.Sin(timer * 50) * rotationAmount);

            yield return null;
        }
        
        float dropTimer = 0f;
        while (dropTimer < 2f)
        {
            dropTimer += Time.deltaTime;
            transform.position += new Vector3(0f, 0.05f * -CurrentGravity, 0f);
            yield return null;
        }

        // 블록 파괴
        this.gameObject.SetActive(false);
    }

    public void ChangeSpeed(float _value)
    {
        dropSpeedR = _value;
    }

    override public void ChangeColdWeather()
    {
        //부서지는 블럭 의 길이를 검사하고 그 블럭의 스프라이트 렌더가 널이 아니면 스프라이트 교체
        SpriteRenderer.sprite = coldSprite;
    }

    public override void ChangeHotWeather()
    {
        SpriteRenderer.sprite = hotSprite;
    }
    public override void DownSizing()
    {
    }
    public override void UpSizing()
    {
    }
}
