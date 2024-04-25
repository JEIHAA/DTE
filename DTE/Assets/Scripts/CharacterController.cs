using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    public delegate void CallbackFunc();
    Dictionary<string, CallbackFunc> dicCommands = new Dictionary<string, CallbackFunc>();
    [SerializeField] private float jumpSpeed = 7f; // 점프 속도
    [SerializeField] private float jumpFallMultiplier = 2f; // 점프 후 빠르게 착지하기 위한 가속도 계수
    [SerializeField] private float speed = 10f;    // 이동 속도
    private bool isJump = false;                  // 점프 상태
    private bool isLadder = false;                // 사다리에 있는지 여부
    private float ladderX;                        // 사다리에서 W를 누를 때의 X 좌표

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 플레이어가 땅에 닿았는지 검사하여 점프 상태를 업데이트합니다.
        if (rb.velocity.y <= 0)
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 2, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.6f)
                {
                    isJump = false;
                }
            }
        }
    }

    private void Update()
    {
        // 좌우 이동
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // 사다리를 타고 올라가거나 내려가는 경우
        if (isLadder)
        {
            float ver = Input.GetAxis("Vertical");
            rb.gravityScale = 0; // 중력을 0으로 설정하여 중력의 영향을 받지 않도록 합니다.
            rb.velocity = new Vector2(rb.velocity.x, ver * speed); // 수직 이동
            if (Input.GetKeyDown(KeyCode.W))
            {
                // W 키를 누를 때의 X 좌표를 저장합니다.
                ladderX = transform.position.x;
            }
            if (Input.GetKey(KeyCode.W))
            {
                // W 키를 누른 상태에서 좌우로 이동할 때, 저장된 X 좌표에서 +-1 범위 내에서만 이동하도록 제한합니다.
                float newX = ladderX + (Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);
                transform.position = new Vector2(Mathf.Clamp(newX, ladderX - 1, ladderX + 1), transform.position.y);
            }
        }
        else
        {
            rb.gravityScale = 1; // 사다리를 탈출하면 중력을 다시 활성화합니다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(); // 점프
            }
        }

        // 점프 후 빠르게 착지하기 위한 처리
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpFallMultiplier - 1) * Time.deltaTime;
        }

    }

    private void Jump()
    {
        // 이미 점프 중이면 함수를 종료합니다.
        if (isJump) return;

        isJump = true; // 점프 상태로 변경합니다.
        rb.velocity = Vector2.up * jumpSpeed; // 점프 속도로 상승합니다.
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        // 땅에 닿았을 때 점프 상태를 해제합니다.
        if (_collision.gameObject.layer == 3)
        {
            isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        // 사다리에 닿았을 때 사다리 상태를 활성화합니다.
        if (_collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        // 사다리에서 벗어났을 때 사다리 상태를 비활성화합니다.
        if (_collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
}
