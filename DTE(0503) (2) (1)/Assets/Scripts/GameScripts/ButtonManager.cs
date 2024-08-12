using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button[] gravityBtns;
    [SerializeField]
    private Button[] sizeBtns;
    [SerializeField]
    private Button[] wheatherBtns;

    private void Start()
    {
        InitBtns(gravityBtns);
        InitBtns(sizeBtns);
        InitBtns(wheatherBtns);
    }

    private void InitBtns(Button[] _btns)
    {
        foreach (Button btn in _btns)
        {
            btn.onClick.AddListener(() =>
            {
                if (btn != null)
                {
                    ClickBtn(_btns);
                    btn.interactable = false;
                }
            });
        }
    }

    public void ClickBtn(Button[] _btns)
    {
        foreach(Button btn in _btns)
        {
           btn.interactable = true;
        }
    }
    

    

}
