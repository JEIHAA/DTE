using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InteractiveManager : MonoBehaviour
{
    public enum INTERACTIVE_TYPE
    { NORMAL_GRAVITY, ZERO_GRAVITY, COUNTER_GRAVITY, SIZE_1, SIZE_2 };
    public enum OBJECT_TYPE
    { ROCK, THORN, BREAKBLOCK, MONSTER ,END, };

    [SerializeField] private GameObject player = null;

    [SerializeField] private GameObject rockHolder = null;
    [SerializeField] private GameObject thornHolder = null;
    [SerializeField] private GameObject breakBlockHolder = null;
    [SerializeField] private GameObject MonsterHolder = null;

    private Dictionary<OBJECT_TYPE, List<InteractiveObject>> all = new Dictionary<OBJECT_TYPE, List<InteractiveObject>>();

    

    private void Awake()
    {
        for(OBJECT_TYPE type = OBJECT_TYPE.ROCK; type != OBJECT_TYPE.END; type++)
        {
            all.Add(type, new List<InteractiveObject>());
        }
    }

    private void Start()
    {
        BringAllChildren(rockHolder.transform, all[OBJECT_TYPE.ROCK]);
        BringAllChildren(thornHolder.transform, all[OBJECT_TYPE.THORN]);
        BringAllChildren(breakBlockHolder.transform, all[OBJECT_TYPE.BREAKBLOCK]);
        BringAllChildren(MonsterHolder.transform, all[OBJECT_TYPE.MONSTER]);
    }

    private void BringAllChildren(Transform _parent, List<InteractiveObject> _arr)
    {
        for(int i = 0; i < _parent.childCount; ++i)
            _arr.Add(_parent.GetChild(i).GetComponentInChildren<InteractiveObject>());
    }


    public void ApplyeGravity_Normal()
    {
        player.GetComponent<InteractiveObject>().NormalGravity();

        foreach(OBJECT_TYPE type in all.Keys)
        {
            foreach (InteractiveObject obj in all[type])
            {
                obj?.NormalGravity();
            }
        }

          
    }

    public void ApplyeGravity_Zero()
    {
        player.GetComponent<InteractiveObject>().ZeroGravity();

        foreach (OBJECT_TYPE type in all.Keys)
        {
            foreach (InteractiveObject obj in all[type])
            {
                obj?.ZeroGravity();
            }
        }
    }

    public void ApplyeGravity_Counter() 
    {
        player.GetComponent<InteractiveObject>().CounterGravity();

        foreach (OBJECT_TYPE type in all.Keys)
        {
            foreach (InteractiveObject obj in all[type])
            {
                obj?.CounterGravity();
            }
        }
    }

    public void ApplyMode1()
    {
        player.GetComponent<InteractiveObject>().DownSizing();

        foreach (OBJECT_TYPE type in all.Keys)
        {
            foreach (InteractiveObject obj in all[type])
            {
                obj?.UpSizing();
            }
        }
    }
    public void ApplyMode2()
    {
        player.GetComponent<InteractiveObject>().UpSizing();

        foreach (OBJECT_TYPE type in all.Keys)
        {
            foreach (InteractiveObject obj in all[type])
            {
                obj?.DownSizing();
            }
        }
    }

    public void ApplyMode3()
    {
        player.GetComponent<InteractiveObject>().NormalSize();

        foreach (OBJECT_TYPE type in all.Keys)
        {
            foreach (InteractiveObject obj in all[type])
            {
                obj?.NormalSize();
            }
        }
    }


    public void ApplyWeather_Hot()
    {
       // player.GetComponent<InteractiveObject>().ChangeHotWeather();

        foreach (OBJECT_TYPE type in all.Keys)
        {
            foreach (InteractiveObject obj in all[type])
            {
                obj?.ChangeHotWeather();
            }
        }
    }

    public void ApplyWeather_Cold()
    {
        // player.GetComponent<InteractiveObject>().ChangeHotWeather();

        foreach (OBJECT_TYPE type in all.Keys)
        {
            foreach (InteractiveObject obj in all[type])
            {
                obj?.ChangeColdWeather();
            }
        }
    }

}
