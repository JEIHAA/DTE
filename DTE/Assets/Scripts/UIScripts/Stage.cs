using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public bool isClear = false; // �������� Ŭ���� ����
    public bool isChallenge = false; // ���� ������ ���� ����
    [SerializeField] private int stageNum;

    private Image buttonImage;

    private void Start()
    {
        // �ڽ��� Image ������Ʈ ��������
        buttonImage = GetComponent<Image>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
    }

    // �̹��� ���� �޼���
    public void ChangeImage(string imageName)
    {

        // �̹��� �ε�
        Texture2D newTexture = Resources.Load(imageName) as Texture2D;
        if (newTexture != null)
        {
            // ��������Ʈ ����
            Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.zero);
            buttonImage.sprite = newSprite;
        }
    }
    private void ChangeScene()
    {
        if (isChallenge)
        {
            SceneManager.LoadScene("Stage" + stageNum);
        }
        else
        {
            Debug.Log("�� ���������� Ŭ���� �Ͽ��� �մϴ�.");
        }
        // �� �̸��� "YourSceneName"�� ������ ��ȯ
        
    }
}
