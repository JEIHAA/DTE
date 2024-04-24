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
            // Player와 플랫폼의 충돌을 무시함
            Physics2D.IgnoreCollision(_collision.GetComponent<Collider2D>(), platformCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            // Player와 플랫폼의 충돌을 다시 활성화함
            Physics2D.IgnoreCollision(_collision.GetComponent<Collider2D>(), platformCollider, false);
        }
    }
}
