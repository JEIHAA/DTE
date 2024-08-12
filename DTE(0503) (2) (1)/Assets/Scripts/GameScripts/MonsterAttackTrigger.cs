using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using monster;
using Character;

public class MonsterAttackTrigger : MonoBehaviour
{
    private MonsterState monster;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        monster = GetComponentInParent<MonsterState>();
        animator = monster.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player") && HasParameter(animator, "isAttack"))
        {
            monster.ChangeState(new AttackState(monster, animator, audioSource));
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {

        if (_collision.CompareTag("Player") && !monster.IsUnSafe)
        {
            monster.ChangeState(new MovingState(monster, animator));
        }
        else if (HasParameter(animator, "isIdle")) { monster.ChangeState(new IdleState(monster, animator)); }
        else { monster.ChangeState(new MovingState(monster, animator)); }
    }

    protected bool HasParameter(Animator _animator, string _paramName)
    {
        return System.Array.Exists(animator.parameters, p => p.name == _paramName);
    }
}
