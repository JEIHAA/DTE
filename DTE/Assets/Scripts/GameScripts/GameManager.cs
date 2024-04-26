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
    [Space(10f)]
    [Header("부서지는 벽 홀더")]
    [SerializeField] private BreakBlockHolder breakBlockHolder;
    [Space(10f)]
    [Header("카메라 오브젝트")]
    [SerializeField] private CameraController cameraController;
    [Space(10f)]
    [Header("사망씬 오브젝트")]
    [SerializeField] private DieCanversController dieCanversController;
    [Space(10f)]
    [Header("뒷 배경 움직임")]
    [SerializeField] private LayerHolder[] layerHolders;


    private void Update()
    {
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
        //카메라 움직임
        cameraController.CameraMoving();

        if (Input.GetMouseButtonDown(0))
        {
            ChangeWeather();
        }
    }
    private void ChangeWeather()
    {

        // 날씨에 따라 적절한 배경을 활성화/비활성화합니다.
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
