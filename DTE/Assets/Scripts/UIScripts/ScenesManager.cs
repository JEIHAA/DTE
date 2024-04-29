using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [Header("���ȭ�� ����")]
    [SerializeField] private GameObject backGroundSun = null;
    [SerializeField] private GameObject backGroundMoon = null;
    [SerializeField] private LayerHolder[] layerHolders = null;
    [Space(10f)]
    [Header("Ÿ��Ʋ �ؽ�Ʈ ����")]
    [SerializeField] private TitleTextUI titleTextUI = null;
    [Space(10f)]
    [Header("Ÿ��Ʋ ������ �ؽ�Ʈ ����")]
    [SerializeField] private TextUpScale textUpScale = null;
    // 0 = �� , 1 = ��
    private bool isWeather;

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
        // �����̽� Ű�� ������ ���� ������ �̵��մϴ�.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
