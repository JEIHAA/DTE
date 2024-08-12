using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidHolder : MonoBehaviour
{
    [SerializeField] private Lava[] lavas;
    [SerializeField] private Water[] waters;

    private void Awake()
    {
        lavas = GetComponentsInChildren<Lava>();
        waters = GetComponentsInChildren<Water>();
    }

    public void Destroy()
    {
        if(lavas != null) { 
            for(int i = 0; i < lavas.Length; i++)
            {
                if (lavas[i] != null)
                {
                    lavas[i].Destroy();
                    Destroy(this.gameObject);
                }
        }
        }
        else if (waters != null) { 
            for (int i = 0; i < waters.Length; i++)
            {
                if (waters[i] != null)
                {
                    lavas[i].Destroy();
                    Destroy(this.gameObject);
                }
            }
        }
    }

}
