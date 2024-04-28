using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    public enum STATE
    {
        IDLE, LEFT, RIGHT, CLIMB, JUMP, LANDING,
    }
    [SerializeField] private float jumpSpeed = 7f;      // 점프 속도
    [SerializeField] private float speed = 10f;         // 이동 속도
    [SerializeField] private float jumpHeight = 2.5f;   // 점프 높이
    [SerializeField] private float landingSpeed = -15f; // 착지 속도
    [SerializeField] private float flyingTime = 2f;     // 착지 속도
    private Rigidbody2D rb;                             // 리지드바디
    private bool isJump = false;                        // 점프 상태
    private bool isLadder = false;                      // 사다리에 있는지 여부
    private float startHeight;                          // 점프시작 위치
    public STATE state;                                 // 현재상태




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(STATE.IDLE);
    }
    private void FixedUpdate()
    {
        if (transform.position.y - startHeight >= jumpHeight) //점프높이 도달시 하강
        {
            Landing();
            startHeight = transform.position.y;
        }
        if (rb.velocity.y < 0 - flyingTime) //떨어질때 속도 보정
            rb.velocity = new Vector2(0f, landingSpeed);
    }
    private void Update()
    {
        MoveLeftRight();// 좌우 이동
        ClimbLadder(); // 사다리타기
        Jump(); // 점프
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
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        // 사다리 내려오기
        else if (isLadder == true && Input.GetKey(KeyCode.S))
        {
            if (CanClimb(rb.position, Vector2.down, 1.5f) == false)
            {
                ChangeState(STATE.IDLE);
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 1f;
                return;
            }
            rb.bodyType = RigidbodyType2D.Kinematic;
            ChangeState(STATE.CLIMB);
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            startHeight = transform.position.y; //점프 시작위치 저장
            isJump = true; // 점프 상태로 변경합니다.
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }
    private void Landing()
    {
        rb.velocity = new Vector2(0f, -1f);
        ChangeState(STATE.LANDING);
    }
    private bool CanClimb(Vector2 _pos, Vector2 _dir, float _dis)
    {
        RaycastHit2D hit = Physics2D.Raycast(_pos, _dir, _dis, LayerMask.GetMask("Ladder"));
        return hit.collider != null;
    }
    private bool CanJump() // 왼발 오른발에서 레이를 쏴서 점프가능한 상황 체크
    {
        if (isJump || state == STATE.LANDING)
            return false;
        RaycastHit2D rayHitRight = Physics2D.Raycast(rb.position + new Vector2(-0.4f, 0f), Vector2.down, 2f, LayerMask.GetMask("Ground"));
        RaycastHit2D rayHitLeft = Physics2D.Raycast(rb.position + new Vector2(0.4f, 0f), Vector2.down, 2f, LayerMask.GetMask("Ground"));
        return (rayHitRight.collider != null || rayHitLeft.collider != null);
    }
    private bool isWall(Vector3 _dir) //머리 가슴 발에서 레이를 쏴서 벽이 있는지 체크
    {
        Vector2 headPos = new Vector3(0f, 1f);
        Vector2 footPos = new Vector3(0f, -1f);
        RaycastHit2D hitMid = Physics2D.Raycast(rb.position, _dir, 0.5f, LayerMask.GetMask("Ground"));
        RaycastHit2D hitHead = Physics2D.Raycast(rb.position + headPos, _dir, 0.5f, LayerMask.GetMask("Ground"));
        RaycastHit2D hitfoot = Physics2D.Raycast(rb.position + footPos, _dir, 0.5f, LayerMask.GetMask("Ground"));
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
            rb.gravityScale = 1f;
            rb.bodyType = RigidbodyType2D.Dynamic;
            state = STATE.IDLE;
            isLadder = false;
        }
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