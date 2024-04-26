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

        // 현재 타일의 원래 스프라이트를 저장
        oldSprite = spriteRenderer.sprite;
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
        // 시작 위치
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float shakeAmount = 0.1f;
        float rotationAmount = 0.1f;
        float dropDistance = 30f; // 떨어질 거리
        float duration = 1f;
        float timer = 0f;
        
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
        float dropSpeed = dropDistance / 6f; // 떨어지는데 걸리는 시간
        float dropTimer = 0f;

        while (dropTimer < dropSpeed)
        {
            dropTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, dropPosition, dropTimer / dropSpeed);
            yield return null;
        }

        // 파괴
        Destroy(gameObject);
    }
}



