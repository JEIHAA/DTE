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
    private float jumpSpeed = 7f;                       // 점프 속도
    private float speed = 10f;                          // 이동 속도
    private float jumpHeight = 2.5f;                    // 점프 높이
    private float landingSpeed = -15f;                  // 착지 속도
    private float flyingTime = 2f;                      // 착지 속도
    private float jumpHeight_Normal = 2.5f;             // 일반중력 점프높이
    private float jumpSpeed_Normal = 7f;                // 일반중력 점프속도
    [SerializeField] float jumpHeight_Zero = 5f;        // 무중력 점프높이
    [SerializeField] float jumpSpeed_Zero = 20f;        // 무중력 점프속도
    private bool isNuckBack = false;
    private bool isJump = false;                        // 점프 상태
    private bool isLadder = false;                      // 사다리에 있는지 여부
    private float startHeight;                          // 점프시작 위치
    public STATE state;                                 // 현재상태
    [SerializeField] Canvas dieUI = null;               // 사망 UI
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
        if (CurrentGravity * (transform.position.y - startHeight) >= jumpHeight) //점프높이 도달시 하강
        {
            Landing();
            startHeight = transform.position.y;
        }
        if (rb.velocity.y * CurrentGravity + flyingTime * CurrentGravity < 0f)        //떨어질때 속도 보정
            rb.velocity = new Vector2(0f, landingSpeed * CurrentGravity);
    }
    private void Update()
    {
        if (isNuckBack)
            return;
        MoveLeftRight();// 좌우 이동
        ClimbLadder(); // 사다리타기
        Jump(); // 점프
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
        // 사다리 올라가기
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
        // 사다리 내려오기
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
            startHeight = transform.position.y; //점프 시작위치 저장
            isJump = true; // 점프 상태로 변경합니다.
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
    private bool CanJump() // 왼발 오른발에서 레이를 쏴서 점프가능한 상황 체크
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
    private bool isWall(Vector3 _dir) //머리 가슴 발에서 레이를 쏴서 벽이 있는지 체크
    {
        float gravityDir = CurrentGravity > 0 ? CurrentGravity : -CurrentGravity; // 일반중력 ? 일반중력 : -일반중력 일반중력이 아닐경우 머리 발 역수로 변경
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
        // 땅에 닿았을 때 점프 상태를 해제합니다.
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
        // 사다리에 닿았을 때 사다리 상태를 활성화합니다.
        if (_collision.CompareTag("Ladder"))
            isLadder = true;
    }
    private void OnTriggerExit2D(Collider2D _collision)
    {
        // 사다리에서 벗어났을 때 사다리 상태를 비활성화합니다.
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
                //IDLE 애니재생
                break;
            case STATE.CLIMB:
                //CLIMB 애니재생
                rb.gravityScale = 0f;
                rb.velocity = Vector3.zero;
                break;
            case STATE.LEFT:
                //LEFT 애니재생
                break;
            case STATE.RIGHT:
                //RIGHT 애니재생
                break;
            case STATE.LANDING:
                //LANDING 애니재생
                break;
            case STATE.JUMP:
                //JUMP 애니재생
                break;
        }
    }
}