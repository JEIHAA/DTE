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
        //�μ����� �� �� ���̸� �˻��ϰ� �� ���� ��������Ʈ ������ ���� �ƴϸ� ��������Ʈ ��ü
        for (int i = 0; i < breakBlocks.Length; i++)
        {
            if(breakBlocks[i].spriteRenderer != null) { 
                breakBlocks[i].spriteRenderer.sprite = breakBlocks[i].newSprite;
            }
        }
    }

    public void ChangeHotWeather()
    {
        //�μ����� �� �� ���̸� �˻��ϰ� �� ���� ��������Ʈ ������ ���� �ƴϸ� ��������Ʈ ��ü
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
