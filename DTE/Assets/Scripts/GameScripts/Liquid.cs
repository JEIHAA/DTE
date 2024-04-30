using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    private float damageInterval = 1f; // �������� ������ ����
    private float timer = 0f; // Ÿ�̸� ����
    public bool isDamage = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isDamage == true)
        {
            // 1�ʸ��� ������ ������
            timer += Time.deltaTime;
            if (timer >= damageInterval)
            {
                // ������ ������ �Լ� ȣ��
                DealDamage();
                timer = 0f; // Ÿ�̸� �ʱ�ȭ
            }
        }
    }

    // �������� ������ �Լ�
    private void DealDamage()
    {
        Debug.Log("�������� �Ծ����ϴ�!");
    }
}
