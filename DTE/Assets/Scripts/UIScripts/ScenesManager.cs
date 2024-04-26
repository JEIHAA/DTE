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
        // Scene�� �ε�� �� �ش� GameObject�� Ž���մϴ�.
        backGroundMoon = GameObject.FindWithTag("BackGroundMoon");
        backGroundSun = GameObject.FindWithTag("BackGroundSun");
    }

    private void Start()
    {
        // ������ �������� �����մϴ�.
        isWeather = (Random.value > 0.5f);

        // ������ ���� ������ ����� Ȱ��ȭ/��Ȱ��ȭ�մϴ�.
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
        // �����̽� Ű�� ������ ���� ������ �̵��մϴ�.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
