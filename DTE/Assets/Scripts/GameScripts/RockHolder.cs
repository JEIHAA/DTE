using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RockHolder : MonoBehaviour
{
    private Transform rock;
    [SerializeField]private SpriteRenderer rocksSpriteRenderer;
    private Vector3 smallSize = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 defaultSize = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 bigSize = new Vector3(1.5f, 1.5f, 1.5f);
    private int sizeState = 1;

    public Sprite newSprite;
    public Sprite oldSprite;
    // 0 스몰사이즈, 1 기본사이즈, 2빅사이즈
    private void Awake()
    {
        rock = GetComponent<Transform>();
        rocksSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        oldSprite = rocksSpriteRenderer.sprite;
    }





    public void ChangeUpScale()
    {
        if (sizeState == 2)
        {
            return;
        }
        else if (sizeState == 1)
        {
            TransScale(bigSize);
            sizeState = 2;
        }
        else if (sizeState == 0)
        {
            TransScale(defaultSize);
            sizeState = 1;
        }
    }

    public void ChangeDownScale()
    {
        if (sizeState == 0)
        {
            return;
        }
        else if (sizeState == 1)
        {
            TransScale(smallSize);
            sizeState = 0;
        }
        else if (sizeState == 2)
        {
            TransScale(defaultSize);
            sizeState = 1;
        }
    }
    private void TransScale(Vector3 _value)
    {
        rock.localScale = _value;
    }


    public void ChangeColdWeather()
    {

        if (rocksSpriteRenderer != null)
        {
            rocksSpriteRenderer.sprite = newSprite;
        }


    }

    public void ChangeHotWeather()
    {
        if (rocksSpriteRenderer != null)
        {
            rocksSpriteRenderer.sprite = oldSprite;
        }
        
    }
}
