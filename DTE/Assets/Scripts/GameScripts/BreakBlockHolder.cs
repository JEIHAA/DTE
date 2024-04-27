using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlockHolder : MonoBehaviour
{
    [SerializeField] private BreakBlock[] breakBlocks;
    private void Awake()

    {
        breakBlocks = GetComponentsInChildren<BreakBlock>();

    }
    public void ChangeColdWeather()
    {
        //부서지는 블럭 의 길이를 검사하고 그 블럭의 스프라이트 렌더가 널이 아니면 스프라이트 교체
        for (int i = 0; i < breakBlocks.Length; i++)
        {
            if(breakBlocks[i].spriteRenderer != null) { 
                breakBlocks[i].spriteRenderer.sprite = breakBlocks[i].newSprite;
            }
        }
    }

    public void ChangeHotWeather()
    {
        //부서지는 블럭 의 길이를 검사하고 그 블럭의 스프라이트 렌더가 널이 아니면 스프라이트 교체
        for (int i = 0; i < breakBlocks.Length; i++)
        {
            if (breakBlocks[i].spriteRenderer != null)
            {
                breakBlocks[i].spriteRenderer.sprite = breakBlocks[i].oldSprite;
            }
        }
    }

    public void ChangeSpeed(float _value)
    {
        for(int i = 0;i < breakBlocks.Length; i++)
        {
            if (breakBlocks[i]!= null)
            {
                breakBlocks[i].ChangeSpeed(_value);
            }
        }
    }
}
