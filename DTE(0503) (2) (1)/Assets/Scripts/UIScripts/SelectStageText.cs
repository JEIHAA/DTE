using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageText : MonoBehaviour
{
    [SerializeField] private float speed = 50f; // 속도
    [SerializeField] private float distance = 30f; // 이동 거리
    [SerializeField] private float startPos = 435.0f; // 시작 위치

    private float pos;

    private void Start()
    {
        pos = startPos;
    }

    public void PingPong()
    {
        pos = startPos + Mathf.PingPong(Time.time * speed, distance); // Mathf.PingPong을 이용해 왕복운동을 합니다.

        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
}
