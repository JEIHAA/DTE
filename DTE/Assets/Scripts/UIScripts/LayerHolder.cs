using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

public class LayerHolder : MonoBehaviour
{
    private ParallaxScroll[] parallaxScrolls;

    private void Start()
    {
        parallaxScrolls = GetComponentsInChildren<ParallaxScroll>();
        for (int i = 0; i < parallaxScrolls.Length; i++)
        {
            parallaxScrolls[i].moveDirection = new Vector3(-1, 0, 0);
        }
    }
    public void MovingBackGround()
    {
        for (int i = 0; i < parallaxScrolls.Length; i++) 
        {
            parallaxScrolls[i].transform.position += parallaxScrolls[i].moveDirection * parallaxScrolls[i].moveSpeed * Time.deltaTime;

            //�������� ���� �̵��ϸ� ��ġ�� Ÿ���� ���������� �缳���մϴ�.
            if (parallaxScrolls[i].transform.position.x <= -parallaxScrolls[i].scrollAmount)
            {
                parallaxScrolls[i].transform.position = parallaxScrolls[i].target.position - parallaxScrolls[i].moveDirection * parallaxScrolls[i].scrollAmount;
            }
        }
    }


}
