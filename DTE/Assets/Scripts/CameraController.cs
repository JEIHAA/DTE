using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 5.0f; // ī�޶� �̵� �ӵ�
    [SerializeField] private float xMinLimit = -21.0f; // X ��ǥ �ּ� ����
    [SerializeField] private float xMaxLimit = 21.3f; // X ��ǥ �ִ� ����
    [SerializeField] private float yMinLimit = -15.0f; // Y ��ǥ �ּ� ����
    [SerializeField] private float yMaxLimit = 15.0f; // Y ��ǥ �ִ� ����

    [SerializeField] private GameObject player; // �÷��̾� ������Ʈ

    private void Update()
    {
        // �÷��̾ ���� ���� ���� ���
        Vector3 dir = player.transform.position - this.transform.position;

        // �̵��� ���� ���
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);

        // X ��ǥ ����
        float newX = Mathf.Clamp(this.transform.position.x + moveVector.x, xMinLimit, xMaxLimit);
        // Y ��ǥ ����
        float newY = Mathf.Clamp(this.transform.position.y + moveVector.y, yMinLimit, yMaxLimit);

        // ���ο� ��ġ�� ī�޶� �̵�
        this.transform.position = new Vector3(newX, newY, this.transform.position.z);
    }
}
