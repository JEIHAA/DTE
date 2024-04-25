using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ãæµ¹");
            StartCoroutine(StartBrokenBlock());
        }
    }

    private IEnumerator StartBrokenBlock()
    {
        // ½ÃÀÛ À§Ä¡
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float shakeAmount = 0.1f;
        float rotationAmount = 0.1f;
        float duration = 1f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // ÁÂ¿ì Èçµé±â
            transform.position = startPosition + new Vector3(Mathf.Sin(timer * 50) * shakeAmount, 0f, 0f);
            // È¸Àü
            transform.rotation = startRotation * Quaternion.Euler(0f, 0f, Mathf.Sin(timer * 50) * rotationAmount);

            yield return null;
        }

        // ÆÄ±«
        Destroy(gameObject);
    }
}



