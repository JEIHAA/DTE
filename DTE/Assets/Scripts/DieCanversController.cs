using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieCanversController : MonoBehaviour
{
    [SerializeField] private bool isDie = false; // 임시 방편
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button titleButton;

    private void Update()
    {
        //플레이어가 죽은상태면
        if (isDie)
        {
            text.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
            titleButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
        }
        // 죽은상태가 아니라면
        else
        {
            text.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            titleButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(false);
        }
    }
    //홈버튼 클릭시 씬 전환
    public void ChangeTitleScene()
    {
        //Title씬 으로 전환
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        //현재씬을 리로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

