using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private GameObject backGroundSun = null;
    [SerializeField] private GameObject backGroundMoon = null;

    private bool isWeather;

    private void Awake()
    {
        // Scene이 로드될 때 해당 GameObject를 탐색합니다.
        backGroundMoon = GameObject.FindWithTag("BackGroundMoon");
        backGroundSun = GameObject.FindWithTag("BackGroundSun");
    }

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
        // 스페이스 키를 누르면 다음 씬으로 이동합니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
