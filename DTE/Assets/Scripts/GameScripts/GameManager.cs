using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private bool isDead = false; // 임시
    private bool weather = false;
    [Header("날씨관련")] // 헤더 이름
    [SerializeField] private GameObject hotActive = null;
    [SerializeField] private GameObject iceActive = null;
    [SerializeField] private GameObject hotBackGround = null;
    [SerializeField] private GameObject iceBackGround = null;
    [Space(5f)]
    [Header("부서지는 벽 홀더")]
    [SerializeField] private BreakBlockHolder breakBlockHolder;
    [SerializeField] private float breakBlockDropSpeed = 10f;
    [Space(5f)]
    [Header("사이즈 바뀌는 바위")]
    [SerializeField] private RockHolder[] rockHolder;
    [Space(5f)]
    [Header("떨어지는 가시")]
    [SerializeField] private DropThronHolder dropThronHolder;
    [SerializeField] private float thronDropSpeed = 2f;
    [Space(5f)]
    [Header("카메라 오브젝트")]
    [SerializeField] private CameraController cameraController;
    [Space(5f)]
    [Header("사망씬 오브젝트")]
    [SerializeField] private DieCanversController dieCanversController;
    [Space(5f)]
    [Header("액체 오브젝트")]
    [SerializeField] LiquidHolder liquidHolder;
    [Space(5f)]
    [Header("뒷 배경 움직임")]
    [SerializeField] private LayerHolder[] layerHolders;
    [Space(5f)]
    [Header("플레이어")]
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
        //떨어지는 가시
        dropThronHolder.MovingThron();
        //카메라 움직임
        cameraController.CameraMoving();
        //뒷 배경 움직임
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
        //사망시 UI 내려옴
        if (isDead)
        {
            dieCanversController.ConversActive(); 
        }
        //임시 날씨 변경
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
        // 날씨에 따라 적절한 배경을 활성화/비활성화합니다.
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