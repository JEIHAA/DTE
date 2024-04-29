using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        string tag = _collision.gameObject.tag;
        // ����� �±׸� ������� ���ϴ� ������ �����մϴ�.
        if (tag == "Player")
        {
            Debug.Log(tag + " �� �浹");
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
