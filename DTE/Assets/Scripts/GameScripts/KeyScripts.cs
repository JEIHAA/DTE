using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScripts : MonoBehaviour
{
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("키를 획득했습니다.");
        }
        //카메라 컨트롤러 함수 추가.
    }
}
