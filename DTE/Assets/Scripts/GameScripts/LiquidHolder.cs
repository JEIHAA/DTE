using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidHolder : MonoBehaviour
{
    private SpriteRenderer[] liquidsSpriterenderer;
    private Liquid[] liquids;

    public Sprite newSprite;
    public Sprite oldSprite;

    private void Awake()
    {
        liquidsSpriterenderer = GetComponentsInChildren<SpriteRenderer>();
        liquids = GetComponentsInChildren<Liquid>();
        oldSprite = liquidsSpriterenderer[0].sprite;
    }





    public void ChangeColdWeather()
    {
        for(int i= 0; i < liquidsSpriterenderer.Length; i++)
        {
            if (liquidsSpriterenderer != null)
            {
                liquidsSpriterenderer[i].sprite = newSprite;
                for(int j = 0; j < liquids.Length; j++)
                {
                    liquids[i].isDamage = false;
                }                
            }
        }
    }

    public void ChangeHotWeather()
    {
        for (int i = 0; i < liquidsSpriterenderer.Length; i++)
        {
            if (liquidsSpriterenderer != null)
            {
                liquidsSpriterenderer[i].sprite = oldSprite;
                for (int j = 0; j < liquids.Length; j++)
                {
                    liquids[i].isDamage = true;
                }                
            }
        }

    }
}


