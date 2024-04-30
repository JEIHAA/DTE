using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
public class CharacterController : InteractiveObject
{
    public enum STATE
    {
        IDLE, LEFT, RIGHT, CLIMB, JUMP, LANDING,
    }
    private float jumpSpeed = 7f;                       // ���� �ӵ�
    private float speed = 10f;                          // �̵� �ӵ�
    private float jumpHeight = 2.5f;                    // ���� ����
    private float landingSpeed = -15f;                  // ���� �ӵ�
    private float flyingTime = 2f;                      // ���� �ӵ�
    private float jumpHeight_Normal = 2.5f;             // �Ϲ��߷� ��������
    private float jumpSpeed_Normal = 7f;                // �Ϲ��߷� �����ӵ�
    [SerializeField] float jumpHeight_Zero = 5f;        // ���߷� ��������
    [SerializeField] float jumpSpeed_Zero = 20f;        // ���߷� �����ӵ�
    private bool isNuckBack = false;
    private bool isJump = false;                        // ���� ����
    private bool isLadder = false;                      // ��ٸ��� �ִ��� ����
    private float startHeight;                          // �������� ��ġ
    public STATE state;                                 // �������
    [SerializeField] Canvas dieUI = null;               // ��� UI
    private int hp = 3;                                // HP
    public int Hp { get { return hp; } }
    private Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        ChangeState(STATE.IDLE);
    }
    public void Init(float _jumpSpeed, float _speed, float _jumpHeight, float _landingSpeed, float _flyingTime)
    {
        jumpSpeed = _jumpSpeed;
        speed = _speed;
        jumpHeight = _jumpHeight;
        landingSpeed = _landingSpeed;
        flyingTime = _flyingTime;
        jumpHeight_Normal = jumpHeight;
        jumpSpeed_Normal = jumpSpeed;
        Debug.Log(jumpSpeed + " " + speed + " " + jumpHeight + " " + landingSpeed + " " + flyingTime);
    }
    private void FixedUpdate()
    {
        if (CurrentGravity * (transform.position.y - startHeight) >= jumpHeight) //�������� ���޽� �ϰ�
        {
            Landing();
            startHeight = transform.position.y;
        }
        if (rb.velocity.y * CurrentGravity + flyingTime * CurrentGravity < 0f)        //�������� �ӵ� ����
            rb.velocity = new Vector2(0f, landingSpeed * CurrentGravity);
    }
    private void Update()
    {
        if (isNuckBack)
            return;
        MoveLeftRight();// �¿� �̵�
        ClimbLadder(); // ��ٸ�Ÿ��
        Jump(); // ����
        if (Input.GetKey(KeyCode.LeftShift))
            ZeroGravity();
        if (Input.GetKey(KeyCode.Z))
            DownSizing();
        if (Input.GetKey(KeyCode.X))
            ResetSize();
        if (Input.GetKey(KeyCode.C))
            UpSizing();
    }
    private void MoveLeftRight()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (isWall(Vector3.left))
                return;
            ChangeState(STATE.LEFT);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (isWall(Vector3.right))
                return;
            ChangeState(STATE.RIGHT);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
    private void ClimbLadder()
    {
        // ��ٸ� �ö󰡱�
        if (isLadder == true && Input.GetKey(KeyCode.W))
        {
            if (CanClimb(rb.position + new Vector2(0f, -1.5f), Vector2.up, 3f) == false)
            {
                ChangeState(STATE.IDLE);
                return;
            }
            ChangeState(STATE.CLIMB);
            transform.position += Vector3.up * CurrentGravity * speed * Time.deltaTime;
        }
        // ��ٸ� ��������
        else if (isLadder == true && Input.GetKey(KeyCode.S))
        {
            if (CanClimb(rb.position, Vector2.down, 1.5f) == false)
            {
                ChangeState(STATE.IDLE);
                rb.bodyType = RigidbodyType2D.Dynamic;
                ResetGravity();
                return;
            }
            rb.bodyType = RigidbodyType2D.Kinematic;
            ChangeState(STATE.CLIMB);
            transform.position += Vector3.down * CurrentGravity * speed * Time.deltaTime;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            startHeight = transform.position.y; //���� ������ġ ����
            isJump = true; // ���� ���·� �����մϴ�.
            rb.AddForce(Vector2.up * CurrentGravity * jumpSpeed, ForceMode2D.Impulse);
        }
    }
    private void Landing()
    {
        rb.velocity = new Vector2(0f, -1f) * CurrentGravity;
        ChangeState(STATE.LANDING);
    }
    private bool CanClimb(Vector2 _pos, Vector2 _dir, float _dis)
    {
        float head = col.bounds.max.y;
        float foot = col.bounds.min.y;
        Vector2 dir;
        float y;
        if (_dir == Vector2.up)
        {
            dir = Vector2.up * CurrentGravity;
            y = foot;
        }
        else
        {
            dir = Vector2.down * CurrentGravity;
            y = head;
        }
        RaycastHit2D rayHit = Physics2D.Raycast(new Vector2(rb.position.x, y), dir, 5.0f, LayerMask.GetMask("Ladder"));
        Debug.DrawRay(new Vector2(rb.position.x, y), dir);
        return rayHit.collider != null;
    }
    private bool CanJump() // �޹� �����߿��� ���̸� ���� ���������� ��Ȳ üũ
    {
        if (isJump || state == STATE.LANDING)
            return false;
        float footY = 0f;
        Vector2 rayDir = Vector2.down;
        if (Vector2.up * CurrentGravity == Vector2.down)
        {
            footY = 1f;
            rayDir = Vector2.up;
        }
        RaycastHit2D rayHitRight = Physics2D.Raycast(rb.position + new Vector2(-(transform.localScale.x * 0.4f), footY), rayDir, 2f, LayerMask.GetMask("Ground"));
        RaycastHit2D rayHitLeft = Physics2D.Raycast(rb.position + new Vector2((transform.localScale.x * 0.4f), footY), rayDir, 2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rb.position + new Vector2(-(transform.localScale.x * 0.4f), 0f), Vector2.down);
        Debug.DrawRay(rb.position + new Vector2((transform.localScale.x * 0.4f), 0f), Vector2.down);
        return (rayHitRight.collider != null || rayHitLeft.collider != null);
    }
    private bool isWall(Vector3 _dir) //�Ӹ� ���� �߿��� ���̸� ���� ���� �ִ��� üũ
    {
        float gravityDir = CurrentGravity > 0 ? CurrentGravity : -CurrentGravity; // �Ϲ��߷� ? �Ϲ��߷� : -�Ϲ��߷� �Ϲ��߷��� �ƴҰ�� �Ӹ� �� ������ ����
        float head = col.bounds.max.y - gravityDir * 0.1f;
        float foot = col.bounds.min.y + gravityDir * 0.1f;
        RaycastHit2D hitMid = Physics2D.Raycast(rb.position, _dir, 0.5f, LayerMask.GetMask("Ground"));
        RaycastHit2D hitHead = Physics2D.Raycast(new Vector2(rb.position.x, head), _dir, 0.5f, LayerMask.GetMask("Ground"));
        RaycastHit2D hitfoot = Physics2D.Raycast(new Vector2(rb.position.x, foot), _dir, 0.5f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rb.position, _dir);
        Debug.DrawRay(new Vector2(rb.position.x, head), _dir);
        Debug.DrawRay(new Vector2(rb.position.x, foot), _dir);
        return (hitMid.collider != null || hitHead.collider != null || hitfoot.collider != null);
    }
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        // ���� ����� �� ���� ���¸� �����մϴ�.
        if (_collision.gameObject.layer == 3)
        {
            if (isJump)
            {
                ChangeState(STATE.IDLE);
                isJump = false;
                return;
            }
            if (state == STATE.CLIMB)
                rb.bodyType = RigidbodyType2D.Kinematic;
            ChangeState(STATE.IDLE);
        }
    }
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        // ��ٸ��� ����� �� ��ٸ� ���¸� Ȱ��ȭ�մϴ�.
        if (_collision.CompareTag("Ladder"))
            isLadder = true;
    }
    private void OnTriggerExit2D(Collider2D _collision)
    {
        // ��ٸ����� ����� �� ��ٸ� ���¸� ��Ȱ��ȭ�մϴ�.
        if (_collision.CompareTag("Ladder"))
        {
            ResetGravity();
            rb.bodyType = RigidbodyType2D.Dynamic;
            state = STATE.IDLE;
            isLadder = false;
        }
    }
    public void Die()
    {
        dieUI.gameObject.SetActive(true);
    }
    private IEnumerator DamageEffect()
    {
        Vector3 curScale = transform.localScale;
        transform.localScale = curScale * 1.2f;
        yield return new WaitForSecondsRealtime(0.3f);
        transform.localScale = curScale;
        isNuckBack = false;
    }
    public void Nuckback(Vector3 _attackPos)
    {
        if (isNuckBack)
            return;
        if (--hp <= 0)
            Die();
        isNuckBack = true;
        Vector3 dir = this.transform.position - _attackPos;
        if (dir.x < 0)
            dir.x = -1;
        else
            dir.x = 1;
        rb.velocity = new Vector2(dir.x * 5f, 0);
        StartCoroutine(DamageEffect());
    }
    override public void ZeroGravity()
    {
        if (GravityState == GRAVITY_STATE.ZERO)
            return;
        base.ZeroGravity();
        SetJumpInfo(jumpHeight_Zero, jumpSpeed_Zero);
        col.offset = new Vector2(0f, -0.24f);
    }
    override public void NormalGravity()
    {
        if (GravityState == GRAVITY_STATE.NORMAL)
            return;
        base.NormalGravity();
        SetJumpInfo(jumpHeight_Normal, jumpSpeed_Normal);
        col.offset = new Vector2(0f, -0.24f);
    }
    override public void CounterGravity()
    {
        if (GravityState == GRAVITY_STATE.COUNTER)
            return;
        base.CounterGravity();
        SetJumpInfo(jumpHeight_Normal, jumpSpeed_Normal);
        col.offset = new Vector2(0f, 0.24f);
    }
    private void SetJumpInfo(float _jumpHeight, float _jumpSpeed)
    {
        jumpHeight = _jumpHeight;
        jumpSpeed = _jumpSpeed;
    }
    public void ChangeState(STATE _state)
    {
        state = _state;
        switch (_state)
        {
            case STATE.IDLE:
                //IDLE �ִ����
                break;
            case STATE.CLIMB:
                //CLIMB �ִ����
                rb.gravityScale = 0f;
                rb.velocity = Vector3.zero;
                break;
            case STATE.LEFT:
                //LEFT �ִ����
                break;
            case STATE.RIGHT:
                //RIGHT �ִ����
                break;
            case STATE.LANDING:
                //LANDING �ִ����
                break;
            case STATE.JUMP:
                //JUMP �ִ����
                break;
        }
    }
}