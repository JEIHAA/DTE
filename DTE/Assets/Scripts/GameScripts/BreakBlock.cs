using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
     public Sprite newSprite;
     public Sprite oldSprite;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ���� Ÿ���� ���� ��������Ʈ�� ����
        oldSprite = spriteRenderer.sprite;
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
        // ���� ��ġ
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float shakeAmount = 0.1f;
        float rotationAmount = 0.1f;
        float dropDistance = 30f; // ������ �Ÿ�
        float duration = 1f;
        float timer = 0f;
        
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
        float dropSpeed = dropDistance / 6f; // �������µ� �ɸ��� �ð�
        float dropTimer = 0f;

        while (dropTimer < dropSpeed)
        {
            dropTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, dropPosition, dropTimer / dropSpeed);
            yield return null;
        }

        // �ı�
        Destroy(gameObject);
    }
}



