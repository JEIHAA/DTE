using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    [SerializeField] private ChangeActive changeActive;
    [SerializeField] private Sprite newSprite;
    private Sprite oldSprite;
    private SpriteRenderer spriteRenderer;

    private bool isOldSprite = true;

    private void Awake()
    {
        changeActive = FindObjectOfType<ChangeActive>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 현재 타일의 원래 스프라이트를 저장
        oldSprite = spriteRenderer.sprite;
    }
    private void Update()
    {
        //true = ice , false = hot
        if (changeActive.weather)
        {
            // 현재 원래의 스프라이트를 새 스프라이트로 교체
            spriteRenderer.sprite = newSprite;
            isOldSprite = false;
            
        }
        else
        {
            // 현재 새 스프라이트를 원래의 스프라이트로 교체
            spriteRenderer.sprite = oldSprite;
            isOldSprite = true;
            
        }
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

        // 파괴
        Destroy(gameObject);
    }
}



