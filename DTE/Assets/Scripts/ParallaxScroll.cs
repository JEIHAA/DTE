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
        // 배경을 왼쪽으로 움직입니다.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //왼쪽으로 많이 이동하면 위치를 타겟의 오른쪽으로 재설정합니다.
        if (transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - moveDirection * scrollAmount;
        }
    }
}
