using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public bool isClear = false; // 스테이지 클리어 여부
    public bool isChallenge = false; // 도전 가능한 상태 여부
    [SerializeField] private int stageNum;

    private Image buttonImage;

    private void Start()
    {
        // 자신의 Image 컴포넌트 가져오기
        buttonImage = GetComponent<Image>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
    }

    // 이미지 변경 메서드
    public void ChangeImage(string imageName)
    {

        // 이미지 로드
        Texture2D newTexture = Resources.Load(imageName) as Texture2D;
        if (newTexture != null)
        {
            // 스프라이트 생성
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
            Debug.Log("전 스테이지를 클리어 하여야 합니다.");
        }
        // 씬 이름이 "YourSceneName"인 씬으로 전환
        
    }
}
