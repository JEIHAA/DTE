using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] KeyScripts keyScripts;


    private void OnTriggerEnter2D(Collider2D _colider)
    {
        if (_colider.gameObject.CompareTag("Player") && keyScripts.isGetKey == true)
        {
             Debug.Log("Clear");
        }
    }
}
