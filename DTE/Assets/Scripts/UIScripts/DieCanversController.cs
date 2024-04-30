using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieCanversController : MonoBehaviour
{
    [SerializeField] private TitleTextUI titleText;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button titleButton;

    public void ConversActive()
    {
            text.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
            titleButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
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

    public void TitleOn()
    {
        titleText.TitleDown();
    }
}

