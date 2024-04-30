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
    [SerializeField] private float smoothTime = 0.3f; // �ε巯�� �̵��� ���� �ð�
    [SerializeField] private float targetSize = 9f; // ī�޶� Ÿ�� ������
    [SerializeField] private GameObject player; // �÷��̾� ������Ʈ
    public bool isMovingToKey = false;
    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        CameraMoving();
    }

    public void GetKeyMoving()
    {
        Vector3 targetPosition = new Vector3(44f, 17f, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, smoothTime * Time.deltaTime);

    }

    public void CameraMoving()
    {
        if (isMovingToKey)
        {
            GetKeyMoving();
        }
        else
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

    public void MoveToKey(bool _bool)
    {
        if (_bool) 
        { 
            isMovingToKey = true;
        }
        else
        {
            isMovingToKey = false;
        }
    }
}
