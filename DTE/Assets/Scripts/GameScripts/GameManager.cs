using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isDead = false; // �ӽ�
    private bool weather = false;
    [Header("��������")] // ��� �̸�
    [SerializeField] private GameObject hotActive = null;
    [SerializeField] private GameObject iceActive = null;
    [SerializeField] private GameObject hotBackGround = null;
    [SerializeField] private GameObject iceBackGround = null;
    [Space(5f)]
    [Header("�μ����� �� Ȧ��")]
    [SerializeField] private BreakBlockHolder breakBlockHolder;
    [SerializeField] private float breakBlockDropSpeed = 10f;
    [Space(5f)]
    [Header("�������� ����")]
    [SerializeField] private DropThronHolder dropThronHolder;
    [SerializeField] private float thronDropSpeed = 2f;
    [Space(5f)]
    [Header("ī�޶� ������Ʈ")]
    [SerializeField] private CameraController cameraController;
    [Space(5f)]
    [Header("����� ������Ʈ")]
    [SerializeField] private DieCanversController dieCanversController;
    [Space(5f)]
    [Header("�� ��� ������")]
    [SerializeField] private LayerHolder[] layerHolders;
    
    private void FixedUpdate()
    {
        breakBlockHolder.ChangeSpeed(breakBlockDropSpeed);
        Debug.Log(breakBlockDropSpeed);
        dropThronHolder.ChangeGravity(thronDropSpeed);
        Debug.Log(thronDropSpeed);
    }

    private void Update()
    {
        dropThronHolder.MovingThron();
        //�� ��� ������
        if (weather)
        {
            layerHolders[1].MovingBackGround();
            iceBackGround.SetActive(true);
            hotBackGround.SetActive(false);
            iceActive.SetActive(true);
            hotActive.SetActive(false);
        }
        else
        {
            layerHolders[0].MovingBackGround();
            hotBackGround.SetActive(true);
            iceBackGround.SetActive(false);
            iceActive.SetActive(false);
            hotActive.SetActive(true);
        }

        //����� UI ������
        if (isDead)
        {
            dieCanversController.ConversActive();
        }
        //ī�޶� ������
        cameraController.CameraMoving();

        if (Input.GetMouseButtonDown(0))
        {
            ChangeWeather();
        }
    }
    private void ChangeWeather()
    {

        // ������ ���� ������ ����� Ȱ��ȭ/��Ȱ��ȭ�մϴ�.
        if (weather)
        {
            breakBlockHolder.ChangeHotWeather();
            dropThronHolder.ChangeHotWeather();
            weather = false;
        }
        else
        {
            breakBlockHolder.ChangeColdWeather();
            dropThronHolder.ChangeColdWeather();
            weather = true;
        }
    }
}
