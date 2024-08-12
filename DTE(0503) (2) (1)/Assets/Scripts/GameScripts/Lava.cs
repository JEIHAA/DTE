using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private float damageInterval = 1f; // 데미지를 입히는 간격
    private float timer = 0f; // 타이머 변수
    private int cntNum = 0;

    private void OnCollisionStay2D(Collision2D _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            // 1초마다 데미지 입히기
            timer += Time.deltaTime;
            if (timer >= damageInterval)
            {
                // 데미지 입히는 함수 호출
                DealDamage();
                timer = 0f; // 타이머 초기화
            }
        }
    }

    // 데미지를 입히는 함수
    private void DealDamage()
    {
        Debug.Log("데미지를 입었습니다!");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
