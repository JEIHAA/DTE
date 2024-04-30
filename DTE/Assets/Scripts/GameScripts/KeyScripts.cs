using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class KeyScripts : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Tilemap[] cloudTilemapsRenderer;
    [SerializeField] private float fadeDuration = 1.5f;
    private float currentFadeTime = 0.0f;
    public bool isGetKey = false;

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GetKeyMoving());
            StartCoroutine(FadeOutClouds());
            isGetKey = true;
        }
    }

    IEnumerator GetKeyMoving()
    {
        Debug.Log("키를 획득했습니다.");

        // 카메라 이동
        cameraController.MoveToKey(true);
        yield return new WaitForSecondsRealtime(2.0f);
        cameraController.MoveToKey(false);
        Destroy(this.gameObject);
    }

    IEnumerator FadeOutClouds()
    {
        while (currentFadeTime < fadeDuration)
        {
            currentFadeTime += Time.deltaTime;
            float normalizedTime = currentFadeTime / fadeDuration;

            foreach (Tilemap cloudTilemapRenderer in cloudTilemapsRenderer)
            {
                if (cloudTilemapRenderer != null)
                {
                    Color color = cloudTilemapRenderer.color;
                    color.a = Mathf.Lerp(1.0f, 0.0f, normalizedTime);
                    cloudTilemapRenderer.color = color;
                }
            }
            yield return null;
        }

        // Fade가 완료된 후에 Renderer 비활성화
        foreach (Tilemap cloudTilemapRenderer in cloudTilemapsRenderer)
        {
            if (cloudTilemapRenderer != null)
            {
                Color color = cloudTilemapRenderer.color;
                color.a = 0.0f;
                cloudTilemapRenderer.color = color;
            }
        }
    }
}
