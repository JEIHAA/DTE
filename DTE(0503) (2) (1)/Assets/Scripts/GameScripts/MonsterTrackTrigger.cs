using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using System.Threading;
using UnityEngine.UIElements;

public class MonsterTrackTrigger : MonoBehaviour
{
    [SerializeField] private float addTrackSpeed = 0f;
    private MonsterState monster;
    private Animator animator;

    private Vector3 playerPos;
    private Vector3 dir;

    private void Awake()
    {
        monster = GetComponentInParent<MonsterState>();
        animator = monster.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        monster.MoveSpeed += addTrackSpeed;
    }

    private void OnTriggerStay2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {   
            playerPos = _other.GetComponent<Transform>().position;

            monster.IsFree = false;
            LookAtPlayer(playerPos);
            
            playerPos.y = monster.transform.position.y;
            dir = playerPos - monster.transform.position;
            dir.Normalize();

            if (!monster.IsUnSafe)
            {
                monster.ChangeState(new MovingState(monster, animator));
                monster.transform.position += dir * monster.MoveSpeed * Time.deltaTime;
            }
            else { monster.ChangeState(new IdleState(monster, animator)); }

        }
    }

    private void LookAtPlayer(Vector3 _playerPos) {
        if (_playerPos.x <= monster.transform.position.x) {
            if (0 > monster.transform.localScale.x)
            {
                //Debug.Log("flip1");
                monster.Turn();
            }
        }else
        {
            if (monster.transform.localScale.x > 0)
            {
                //Debug.Log("flip2");
                monster.Turn();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        monster.MoveSpeed -= addTrackSpeed;
        monster.IsFree = true;
    }
}
