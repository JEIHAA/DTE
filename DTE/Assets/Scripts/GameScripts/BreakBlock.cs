using System.Collections;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    public Sprite newSprite; // ���� ����� ��������Ʈ
    public Sprite oldSprite; // ���� ����� ��������Ʈ
    [HideInInspector] public SpriteRenderer spriteRenderer;

    // ���� ��ġ�� ȸ����
    private Vector3 startPosition;
    private Quaternion startRotation;

    // ����ũ ����
    [SerializeField] private float shakeAmount = 0.1f; // ��鸲 ����
    public float rotationAmount = 0.1f; // ȸ����
    public float dropDistance = 30f; // ������ �Ÿ�
    public float duration = 1f; // �������� �ð�
    public float dropSpeedR = 10f; // �������� �ð�
    public float timer = 0f; // �ð�

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ���� Ÿ���� ���� ��������Ʈ�� ����
        oldSprite = spriteRenderer.sprite;

        // �ʱ� ��ġ�� ȸ���� ����
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�浹");
            StartCoroutine(StartBrokenBlock());
        }
    }

    private IEnumerator StartBrokenBlock()
    {
        // ������ �ð� ���� ����� ����
        while (timer < duration)
        {
            timer += Time.deltaTime;
            // �¿� ����
            transform.position = startPosition + new Vector3(Mathf.Sin(timer * 50) * shakeAmount, 0f, 0f);
            // ȸ��
            transform.rotation = startRotation * Quaternion.Euler(0f, 0f, Mathf.Sin(timer * 50) * rotationAmount);

            yield return null;
        }

        // �Ʒ��� ��������
        Vector3 dropPosition = startPosition - new Vector3(0f, dropDistance, 0f);
        float dropSpeed = dropDistance / dropSpeedR; // �������µ� �ɸ��� �ð�
        float dropTimer = 0f;

        while (dropTimer < dropSpeed)
        {
            dropTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, dropPosition, dropTimer / dropSpeed);
            yield return null;
        }

        // ��� �ı�
        Destroy(gameObject);
    }

    public void ChangeSpeed(float _value)
    {
        dropSpeedR = _value;
    }
}
