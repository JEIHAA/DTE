using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScripts : MonoBehaviour
{
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Ű�� ȹ���߽��ϴ�.");
        }
        //ī�޶� ��Ʈ�ѷ� �Լ� �߰�.
    }
}
