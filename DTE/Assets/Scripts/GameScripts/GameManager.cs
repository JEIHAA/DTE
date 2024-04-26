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
    [Space(10f)]
    [Header("�μ����� �� Ȧ��")]
    [SerializeField] private BreakBlockHolder breakBlockHolder;
    [Space(10f)]
    [Header("ī�޶� ������Ʈ")]
    [SerializeField] private CameraController cameraController;
    [Space(10f)]
    [Header("����� ������Ʈ")]
    [SerializeField] private DieCanversController dieCanversController;
    [Space(10f)]
    [Header("�� ��� ������")]
    [SerializeField] private LayerHolder[] layerHolders;


    private void Update()
    {
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
            weather = false;
        }
        else
        {
            breakBlockHolder.ChangeColdWeather();
            weather = true;
        }
    }
}
