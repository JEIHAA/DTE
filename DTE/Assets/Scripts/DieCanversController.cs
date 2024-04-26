using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieCanversController : MonoBehaviour
{
    [SerializeField] private bool isDie = false; // �ӽ� ����
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button titleButton;

    private void Update()
    {
        //�÷��̾ �������¸�
        if (isDie)
        {
            text.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
            titleButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
        }
        // �������°� �ƴ϶��
        else
        {
            text.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            titleButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(false);
        }
    }
    //Ȩ��ư Ŭ���� �� ��ȯ
    public void ChangeTitleScene()
    {
        //Title�� ���� ��ȯ
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        //������� ���ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

