using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed; // 카메라 이동 속도
    [SerializeField] private float xMinLimit; // X 좌표 최소 제한
    [SerializeField] private float xMaxLimit; // X 좌표 최대 제한
    [SerializeField] private float yMinLimit; // Y 좌표 최소 제한
    [SerializeField] private float yMaxLimit; // Y 좌표 최대 제한

    [SerializeField] private GameObject player; // 플레이어 오브젝트

    public void CameraMoving()
    {
        // 플레이어를 향한 방향 벡터 계산
        Vector3 dir = player.transform.position - this.transform.position;

        // 이동할 벡터 계산
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);

        // X 좌표 제한
        float newX = Mathf.Clamp(this.transform.position.x + moveVector.x, xMinLimit, xMaxLimit);
        // Y 좌표 제한
        float newY = Mathf.Clamp(this.transform.position.y + moveVector.y, yMinLimit, yMaxLimit);

        // 새로운 위치로 카메라 이동
        this.transform.position = new Vector3(newX, newY, this.transform.position.z);
    }
}
