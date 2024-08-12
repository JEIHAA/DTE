using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        string tag = _collision.gameObject.tag;
        // 검출된 태그를 기반으로 원하는 동작을 수행합니다.
        if (tag == "Player")
        {
            Debug.Log(tag + " 와 충돌");
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (gameObject.CompareTag("Thron") && _collision.gameObject.CompareTag("Rock"))
        {
            Destroy(_collision.gameObject);
            Destroy(gameObject);
        }
    }
}
