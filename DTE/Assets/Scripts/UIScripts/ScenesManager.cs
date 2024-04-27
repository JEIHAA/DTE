using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [Header("배경화면 관리")]
    [SerializeField] private GameObject backGroundSun = null;
    [SerializeField] private GameObject backGroundMoon = null;
    [SerializeField] private LayerHolder[] layerHolders = null;
    [Space(10f)]
    [Header("타이틀 텍스트 관리")]
    [SerializeField] private TitleTextUI titleTextUI = null;
    [Space(10f)]
    [Header("타이틀 프레스 텍스트 관리")]
    [SerializeField] private TextUpScale textUpScale = null;
    // 0 = 문 , 1 = 선
    private bool isWeather;

    private void Start()
    {
        // 날씨를 무작위로 설정합니다.
        isWeather = (Random.value > 0.5f);

        // 날씨에 따라 적절한 배경을 활성화/비활성화합니다.
        if (isWeather)
        {
            backGroundSun.SetActive(false);
        }
        else
        {
            backGroundMoon.SetActive(false);
        }
    }

    private void Update()
    {
        textUpScale.TextScaleUpDown();
        titleTextUI.TitleDown();


        if (isWeather)
        {
            layerHolders[1].MovingBackGround();
        }
        else
        {
            layerHolders[0].MovingBackGround();
        }
        // 스페이스 키를 누르면 다음 씬으로 이동합니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
