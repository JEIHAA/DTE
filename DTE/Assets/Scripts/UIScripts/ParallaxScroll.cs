using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float scrollAmount;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 moveDirection;

    private void Awake()
    {
        moveDirection = new Vector3(-1, 0, 0);
    }
    private void Update()
    {
        // ����� �������� �����Դϴ�.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //�������� ���� �̵��ϸ� ��ġ�� Ÿ���� ���������� �缳���մϴ�.
        if (transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - moveDirection * scrollAmount;
        }
    }
}
