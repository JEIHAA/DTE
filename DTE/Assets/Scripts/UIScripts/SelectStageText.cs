using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageText : MonoBehaviour
{
    [SerializeField] private float speed = 50f; // �ӵ�
    [SerializeField] private float distance = 30f; // �̵� �Ÿ�
    [SerializeField] private float startPos = 435.0f; // ���� ��ġ

    private float pos;

    private void Start()
    {
        pos = startPos;
    }

    public void PingPong()
    {
        pos = startPos + Mathf.PingPong(Time.time * speed, distance); // Mathf.PingPong�� �̿��� �պ���� �մϴ�.

        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
}
