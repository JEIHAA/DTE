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
    [SerializeField] private float jumpSpeed = 7f;      // ���� �ӵ�
    [SerializeField] private float speed = 10f;         // �̵� �ӵ�
    [SerializeField] private float jumpHeight = 2.5f;   // ���� ����
    [SerializeField] private float landingSpeed = -15f; // ���� �ӵ�
    [SerializeField] private float flyingTime = 2f;     // ���� �ӵ�
    private Rigidbody2D rb;                             // ������ٵ�
    private bool isJump = false;                        // ���� ����
    private bool isLadder = false;                      // ��ٸ��� �ִ��� ����
    private float startHeight;                          // �������� ��ġ
    public STATE state;                                 // �������




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(STATE.IDLE);
    }
    private void FixedUpdate()
    {
        if (transform.position.y - startHeight >= jumpHeight) //�������� ���޽� �ϰ�
        {
            Landing();
            startHeight = transform.position.y;
        }
        if (rb.velocity.y < 0 - flyingTime) //�������� �ӵ� ����
            rb.velocity = new Vector2(0f, landingSpeed);
    }
    private void Update()
    {
        MoveLeftRight();// �¿� �̵�
        ClimbLadder(); // ��ٸ�Ÿ��
        Jump(); // ����
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
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        // ��ٸ� ��������
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
            startHeight = transform.position.y; //���� ������ġ ����
            isJump = true; // ���� ���·� �����մϴ�.
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
    private bool CanJump() // �޹� �����߿��� ���̸� ���� ���������� ��Ȳ üũ
    {
        if (isJump || state == STATE.LANDING)
            return false;
        RaycastHit2D rayHitRight = Physics2D.Raycast(rb.position + new Vector2(-0.4f, 0f), Vector2.down, 2f, LayerMask.GetMask("Ground"));
        RaycastHit2D rayHitLeft = Physics2D.Raycast(rb.position + new Vector2(0.4f, 0f), Vector2.down, 2f, LayerMask.GetMask("Ground"));
        return (rayHitRight.collider != null || rayHitLeft.collider != null);
    }
    private bool isWall(Vector3 _dir) //�Ӹ� ���� �߿��� ���̸� ���� ���� �ִ��� üũ
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