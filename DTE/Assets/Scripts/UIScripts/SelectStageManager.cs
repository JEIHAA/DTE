using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageManager : MonoBehaviour
{
    [SerializeField] private SelectStageText selectStageText;
    [SerializeField] private LayerHolder layerHolder;
    private void Update()
    {
        selectStageText.PingPong();
        layerHolder.MovingBackGround();
    }
}
