using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("사이즈 바뀌는 바위")]
    [SerializeField] private Rock[] rockHolder;
    [Space(5f)]
    [Header("카메라 오브젝트")]
    [SerializeField] private CameraController cameraController;
    [Space(5f)]
    [Header("사망문구")]
    [SerializeField] private DieCanversController dieCanversController;
    [Space(5f)]
    [Header("뒷 배경 움직임")]
    [SerializeField] private LayerHolder[] layerHolders;
    [Space(5f)]
    [Header("액체 홀더")]
    [SerializeField] private LiquidHolder[] liquidHolder;
    [Space(5f)]
    [Header("인터렉티브 매니저")]
    [SerializeField] private GameObject InteractiveManager;
    [Space(5f)]
    [Header("버튼")]
    [SerializeField] private Button[] startBtns;
    private AudioSource changeWeatherSound;
    private int changeNum = 0;
    private void Awake()
    {
        changeWeatherSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        foreach(Button button in startBtns)
        {
            button.interactable = false;
        }
    }


    private void Update()
    {
        //사망 문구
        dieCanversController.TextOn();
        
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
       
    }


    public void ChangeWeather_Hot()
    {
        if (weather)
        {
            changeWeatherSound.Play();
            changeNum++;
        }

        for(int i = 0; i<liquidHolder.Length; i++)
        {
            if (changeNum > 3 && liquidHolder[i] != null)
            {
                Debug.Log(changeNum);
                liquidHolder[i].Destroy();
            }
        }
        
        weather = false;
        for (int i = 0; i < rockHolder.Length; i++)
        {
            rockHolder[i].ChangeHotWeather();

        }
    }

    public void ChangeWeather_Cold()
    {
        
        if (!weather)
        {
            changeWeatherSound.Play();
            changeNum++;
        }
        for (int i = 0; i < liquidHolder.Length; i++)
        {
            if (changeNum > 3 && liquidHolder[i] != null)
            {
                Debug.Log(changeNum);
                liquidHolder[i].Destroy();
            }
        }
        weather = true;
        for (int i = 0; i < rockHolder.Length; i++)
        {
            rockHolder[i].ChangeColdWeather();
            
        }
    }
    
    
}
