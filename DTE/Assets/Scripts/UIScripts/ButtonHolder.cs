using UnityEngine;
using UnityEngine.UI;

public class ButtonHolder : MonoBehaviour
{
    public Button[] stages; // 스테이지 오브젝트 배열

    private void Start()
    {
        // 모든 스테이지의 컴포넌트를 받아옴
        for (int i = 0; i < stages.Length; i++)
        {
            Stage stage = stages[i].GetComponent<Stage>();
            if (stage != null)
            {
                // 전 스테이지의 isClear가 true이면
                if (i > 0 && stages[i - 1].GetComponent<Stage>().isClear)
                {
                    // 다음 스테이지의 isChallenge를 true로 변경하고 스프라이트 변경
                    stage.isChallenge = true;
                    stage.ChangeImage("Buttons/Button_Green");
                }
                else
                {
                    if (stage.isChallenge)
                    {
                        stage.ChangeImage("Buttons/Button_Green");
                    }
                }
            }
            else
            {
                Debug.LogError("Stage component is not found on button " + i);
            }
        }
    }
}
