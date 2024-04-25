using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    public delegate void CallbackFunc();
    Dictionary<string, CallbackFunc> dicCommands = new Dictionary<string, CallbackFunc>();
    [SerializeField] private float jumpSpeed = 7f; // ���� �ӵ�
    [SerializeField] private float jumpFallMultiplier = 2f; // ���� �� ������ �����ϱ� ���� ���ӵ� ���
    [SerializeField] private float speed = 10f;    // �̵� �ӵ�
    private bool isJump = false;                  // ���� ����
    private bool isLadder = false;                // ��ٸ��� �ִ��� ����
    private float ladderX;                        // ��ٸ����� W�� ���� ���� X ��ǥ

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // �÷��̾ ���� ��Ҵ��� �˻��Ͽ� ���� ���¸� ������Ʈ�մϴ�.
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
        // �¿� �̵�
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // ��ٸ��� Ÿ�� �ö󰡰ų� �������� ���
        if (isLadder)
        {
            float ver = Input.GetAxis("Vertical");
            rb.gravityScale = 0; // �߷��� 0���� �����Ͽ� �߷��� ������ ���� �ʵ��� �մϴ�.
            rb.velocity = new Vector2(rb.velocity.x, ver * speed); // ���� �̵�
            if (Input.GetKeyDown(KeyCode.W))
            {
                // W Ű�� ���� ���� X ��ǥ�� �����մϴ�.
                ladderX = transform.position.x;
            }
            if (Input.GetKey(KeyCode.W))
            {
                // W Ű�� ���� ���¿��� �¿�� �̵��� ��, ����� X ��ǥ���� +-1 ���� �������� �̵��ϵ��� �����մϴ�.
                float newX = ladderX + (Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);
                transform.position = new Vector2(Mathf.Clamp(newX, ladderX - 1, ladderX + 1), transform.position.y);
            }
        }
        else
        {
            rb.gravityScale = 1; // ��ٸ��� Ż���ϸ� �߷��� �ٽ� Ȱ��ȭ�մϴ�.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(); // ����
            }
        }

        // ���� �� ������ �����ϱ� ���� ó��
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpFallMultiplier - 1) * Time.deltaTime;
        }

    }

    private void Jump()
    {
        // �̹� ���� ���̸� �Լ��� �����մϴ�.
        if (isJump) return;

        isJump = true; // ���� ���·� �����մϴ�.
        rb.velocity = Vector2.up * jumpSpeed; // ���� �ӵ��� ����մϴ�.
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        // ���� ����� �� ���� ���¸� �����մϴ�.
        if (_collision.gameObject.layer == 3)
        {
            isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        // ��ٸ��� ����� �� ��ٸ� ���¸� Ȱ��ȭ�մϴ�.
        if (_collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        // ��ٸ����� ����� �� ��ٸ� ���¸� ��Ȱ��ȭ�մϴ�.
        if (_collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
}
