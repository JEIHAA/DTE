using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed; // ī�޶� �̵� �ӵ�
    [SerializeField] private float xMinLimit; // X ��ǥ �ּ� ����
    [SerializeField] private float xMaxLimit; // X ��ǥ �ִ� ����
    [SerializeField] private float yMinLimit; // Y ��ǥ �ּ� ����
    [SerializeField] private float yMaxLimit; // Y ��ǥ �ִ� ����

    [SerializeField] private GameObject player; // �÷��̾� ������Ʈ

    public void CameraMoving()
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
