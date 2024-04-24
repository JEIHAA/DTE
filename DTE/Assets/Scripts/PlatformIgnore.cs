using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    [SerializeField] private Collider2D platformCollider; 

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            // Player�� �÷����� �浹�� ������
            Physics2D.IgnoreCollision(_collision.GetComponent<Collider2D>(), platformCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            // Player�� �÷����� �浹�� �ٽ� Ȱ��ȭ��
            Physics2D.IgnoreCollision(_collision.GetComponent<Collider2D>(), platformCollider, false);
        }
    }
}
