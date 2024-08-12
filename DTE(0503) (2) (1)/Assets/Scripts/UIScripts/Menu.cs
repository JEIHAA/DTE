using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadTitleScenes()
    {
        //타이틀 씬 전환
        SceneManager.LoadScene(0);
    }
    public void LoadSelectStageScenes()
    {
        //스테이지 선택 씬
        SceneManager.LoadScene(1);
    }

    public void Retry()
    {
        //현재씬을 리로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
