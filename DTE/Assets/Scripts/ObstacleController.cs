using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        string tag = _collision.gameObject.tag;
        Debug.Log(tag);

        // ����� �±׸� ������� ���ϴ� ������ �����մϴ�.
        if (tag == "Player")
        {
            Debug.Log(tag + " �� �浹");
        }
    }
}
