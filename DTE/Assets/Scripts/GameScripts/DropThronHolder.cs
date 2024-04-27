using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropThronHolder : MonoBehaviour
{
    [SerializeField] private DropThron[] dropThrons;

    private void Awake()
    {
        dropThrons = GetComponentsInChildren<DropThron>();
    }

    private void Start()
    {
        for(int i = 0; i < dropThrons.Length; i++) {
            Debug.Log(dropThrons[i]);
        }
    }

    public void MovingThron()
    {
        for(int i = 0; i <  dropThrons.Length; i++) 
        {
            if (dropThrons[i] != null) { 
            dropThrons[i].MovingDrop();
            }
        }
    }

    public void ChangeColdWeather()
    {
        //�μ����� �� �� ���̸� �˻��ϰ� �� ���� ��������Ʈ ������ ���� �ƴϸ� ��������Ʈ ��ü
        for (int i = 0; i < dropThrons.Length; i++)
        {
            if (dropThrons[i].spriteRenderer != null)
            {
                dropThrons[i].spriteRenderer.sprite = dropThrons[i].newSprite;
            }
        }
    }

    public void ChangeHotWeather()
    {
        //�μ����� �� �� ���̸� �˻��ϰ� �� ���� ��������Ʈ ������ ���� �ƴϸ� ��������Ʈ ��ü
        for (int i = 0; i < dropThrons.Length; i++)
        {
            if (dropThrons[i].spriteRenderer != null)
            {
                dropThrons[i].spriteRenderer.sprite = dropThrons[i].oldSprite;
            }
        }
    }

    public void ChangeGravity(float _value)
    {
        for (int i = 0; i < dropThrons.Length; i++)
        {
            if (dropThrons[i] != null)
            {
                dropThrons[i].ChangeGravity(_value);
            }
        }
    }
}
