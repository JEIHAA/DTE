using UnityEngine;
using UnityEngine.UI;

public class ButtonHolder : MonoBehaviour
{
    public Button[] stages; // �������� ������Ʈ �迭

    private void Start()
    {
        // ��� ���������� ������Ʈ�� �޾ƿ�
        for (int i = 0; i < stages.Length; i++)
        {
            Stage stage = stages[i].GetComponent<Stage>();
            if (stage != null)
            {
                // �� ���������� isClear�� true�̸�
                if (i > 0 && stages[i - 1].GetComponent<Stage>().isClear)
                {
                    // ���� ���������� isChallenge�� true�� �����ϰ� ��������Ʈ ����
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
