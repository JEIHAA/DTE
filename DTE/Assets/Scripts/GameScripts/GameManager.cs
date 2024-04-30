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
    [Header("������ �ٲ�� ����")]
    [SerializeField] private RockHolder[] rockHolder;
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
    [Header("��ü ������Ʈ")]
    [SerializeField] LiquidHolder liquidHolder;
    [Space(5f)]
    [Header("�� ��� ������")]
    [SerializeField] private LayerHolder[] layerHolders;
    [Space(5f)]
    [Header("�÷��̾�")]
    [SerializeField] private GameObject player;
    [SerializeField] private float playerJumpSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpHeight;
    [SerializeField] private float playerLandingSpeed;
    [SerializeField] private float playerFlyingTime;
    
    private void Start()
    {
        player.GetComponent<CharacterController>().Init(playerJumpSpeed, playerSpeed, playerJumpHeight, playerLandingSpeed, playerFlyingTime);
    }
    private void FixedUpdate()
    {
        breakBlockHolder.ChangeSpeed(breakBlockDropSpeed);
        dropThronHolder.ChangeGravity(thronDropSpeed);
    }
    private void Update()
    {
        dieCanversController.TitleOn();
        //�������� ����
        dropThronHolder.MovingThron();
        //ī�޶� ������
        cameraController.CameraMoving();
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
        //�ӽ� ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            ChangeWeather();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < rockHolder.Length; i++){
                rockHolder[i].ChangeDownScale();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < rockHolder.Length; i++)
            {
                rockHolder[i].ChangeUpScale();
            }
        }
    }
    private void ChangeWeather()
    {
        // ������ ���� ������ ����� Ȱ��ȭ/��Ȱ��ȭ�մϴ�.
        if (weather)
        {
            breakBlockHolder.ChangeHotWeather();
            dropThronHolder.ChangeHotWeather();
            liquidHolder.ChangeHotWeather();
            weather = false;
            for (int i = 0; i < rockHolder.Length; i++)
            {
                rockHolder[i].ChangeHotWeather();
            }
        }
        else
        {
            breakBlockHolder.ChangeColdWeather();
            dropThronHolder.ChangeColdWeather();
            liquidHolder.ChangeColdWeather();
            weather = true;
            for (int i = 0; i < rockHolder.Length; i++)
            {
                rockHolder[i].ChangeColdWeather();
            }
        }
    }
}