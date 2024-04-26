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

        // ���� Ÿ���� ���� ��������Ʈ�� ����
        oldSprite = spriteRenderer.sprite;
    }
    private void Update()
    {
        //true = ice , false = hot
        if (changeActive.weather)
        {
            // ���� ������ ��������Ʈ�� �� ��������Ʈ�� ��ü
            spriteRenderer.sprite = newSprite;
            isOldSprite = false;
            
        }
        else
        {
            // ���� �� ��������Ʈ�� ������ ��������Ʈ�� ��ü
            spriteRenderer.sprite = oldSprite;
            isOldSprite = true;
            
        }
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

        // �ı�
        Destroy(gameObject);
    }
}



