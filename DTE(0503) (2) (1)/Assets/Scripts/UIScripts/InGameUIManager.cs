using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;
    [SerializeField]private Menu menu;
    private bool active = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!active)
            {
                menu.gameObject.SetActive(true);
                active = true;
                for(int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(false);
                }
            }            
            else if (active) 
            {
                active = false;
                menu.gameObject.SetActive(false);
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(true);
                }
            }
        }
    }
}
