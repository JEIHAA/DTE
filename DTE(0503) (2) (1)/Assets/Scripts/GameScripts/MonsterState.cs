using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class MonsterState : InteractiveObject
{
    [SerializeField] private float moveSpeed = 2f; // ������ �̵� �ӵ�
    
    public float MoveSpeed {get { return moveSpeed; } set { moveSpeed = value; } }

    private AudioSource audio;
    private Animator animator;

    private Vector3 direction = Vector3.zero; // �̵� ���� ����
    private Vector2 rayOffset;

   
    private bool isFree = true;
    public bool IsFree { get { return isFree; } set { isFree = value; } }
    private bool isUnSafe = true;
    public bool IsUnSafe { get { return isUnSafe; } set { isUnSafe = value; }  }
    private float distance;
    private float dirOffset = -1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        direction.x = dirOffset;
        rayOffset = new Vector2(rb.position.x + dirOffset * 1.2f, rb.position.y - 0.5f * CurrentGravity);
        if (isFree)
        {
            MonsterMove(direction);
        }
        else { isUnSafe = HasObstacles(direction, rayOffset); }
    }

    private void MonsterMove(Vector3 _direction) {

        if (HasObstacles(_direction, rayOffset)) // ���� �տ� ��ֹ��� �ִٸ�
        {
            Turn();
        }
        else { transform.position += _direction * moveSpeed * Time.deltaTime; } // �̵�
    }

    public void Turn()
    {
        dirOffset *= -1;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private bool HasObstacles(Vector3 _direction, Vector2 _rayOffset)
    {
        Vector2 dir = new Vector2(_direction.x, 0);
        RaycastHit2D hitGround = Physics2D.Raycast(_rayOffset, Vector2.down * CurrentGravity, 1f, LayerMask.GetMask("Ground"));
        RaycastHit2D hitThorn = Physics2D.Raycast(_rayOffset, dir, 0.7f, LayerMask.GetMask("Thorn"));
        Debug.DrawRay(_rayOffset, Vector2.down * CurrentGravity, Color.blue);
        Debug.DrawRay(_rayOffset, dir * 0.5f, Color.blue);
        if ((hitGround.collider == null) || (hitThorn.collider != null)) { return true; }
        else { return false; }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Debug.Log(_collision.gameObject.name);
        if (_collision.gameObject.CompareTag("Thron"))
        {
            audio.Play();
            ChangeState(new DieState(this, animator));
            StartCoroutine(MonsterDie());
        }

        if (_collision.gameObject.CompareTag("Rock"))
        {
            audio.Play();
            ChangeState(new DieState(this, animator));
            StartCoroutine(MonsterDie());
        }
    }

    private IEnumerator MonsterDie() {
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }
}

namespace monster { 

    public class AttackState : ICharacterState
    {
        private MonsterState monster;
        private Animator animator;
        private AudioSource audioSource;

        public AttackState(MonsterState _monster, Animator _animator, AudioSource _audioSource)
        {
            this.monster = _monster;
            this.animator = _animator;
            this.audioSource = _audioSource;
        }

        public void EnterState()
        {
            Debug.Log("Monster Attack");
            animator.SetBool("isAttack", true);

        }

        public void ExitState()
        {
            animator.SetBool("isAttack", false);
        }

        public void UpdateState()
        {

        }
    }
}
