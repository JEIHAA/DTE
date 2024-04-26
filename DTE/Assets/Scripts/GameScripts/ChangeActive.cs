using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ChangeActive : MonoBehaviour
{
    [SerializeField] private GameObject hot = null;
    [SerializeField] private GameObject ice = null;
    [SerializeField] private GameObject hotBackGround = null;
    [SerializeField] private GameObject iceBackGround = null;
    public bool weather = false;

    //false = hot, true = ice


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (weather)
            {
                weather = false;
            }
            else
            {
                weather = true;
            }
        }

        // ������ ���� ������ ����� Ȱ��ȭ/��Ȱ��ȭ�մϴ�.
        if (weather)
        {
            ice.SetActive(true);
            iceBackGround.SetActive(true);
            hot.SetActive(false);            
            hotBackGround.SetActive(false);
        }
        else
        {
            hot.SetActive(true);
            hotBackGround.SetActive(true);
            ice.SetActive(false);
            iceBackGround.SetActive(false);
        }
    }
}
