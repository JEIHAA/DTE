using Character;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static InteractiveObject;

public class CharacterController : InteractiveObject
{
    public enum STATE
    {
        IDLE, LEFT, RIGHT, CLIMB, JUMP, LANDING,
    }

    [Header("현재상태")]
    [SerializeField] private float currentSpeed = 10f;                   // 이동속도
    [SerializeField] private float currentJumpPower = 7f;                // 점프파워
    [SerializeField] private float currentJumpHeight = 2.5f;             // 점프높이
    [SerializeField] private float currentLandingSpeed = -15f;           // 착지속도
    [SerializeField] private float flyingTime = 2f;                      // 체공시간
    [Header("얼음위에서")]
    [SerializeField] private Vector2 iceMoveForce = Vector2.zero;                // 빙판에서 미끄러짐 정도
    [SerializeField] private float iceMoveSpeed = 0.00003f;                      // 얼음에서의 프레임당 이동거리    
    [SerializeField] private float maxIceMoveSpeed = 0.3f;                       // 빙판에서의 최대속력
    [SerializeField] private float friction = 0.000004f;                         // 마찰력

    private Animator animator;
    private PlayerState playerState = new PlayerState();

    public float CurrentSpeed { get { return currentSpeed; } set { currentSpeed = value; } }
    public float CurrentJumpPower { get { return currentJumpPower; } set { currentJumpPower = value; } }
    public float CurrentJumpHeight { get { return currentJumpHeight; } set { currentJumpHeight = value; } }
    public float CurrentLandingSpeed { get { return currentLandingSpeed; } set { currentLandingSpeed = value; } }
    public float FlyingTime { get { return flyingTime; } set { flyingTime = value; } }


    [Header("사망UI")]
    [SerializeField] private Canvas dieUI = null;                        // 사망 UI

    private float groundGravity = 1f;                   //일반중력(역중력일 경우 사다리탈때 일시적으로 중력변경)
    private bool isNuckBack = false;                    // 넉백 상태
    private bool isJump = false;                        // 점프 상태
    private bool isLadder = false;                      // 사다리에 있는지 여부
    private float startHeight;                          // 점프시작 위치
    public STATE state;                                 // 현재상태
    private Collider2D col;                             //콜라이더
    private int hp = 3;                                 // HP


    private bool isIce = false;

    //private bool prevIsIce = false;

    public int Hp { get { return hp; } }


    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    private void Start()
    {
        playerState.SetTool(animator, spriteRenderer);
        playerState.IdleState();
        state = STATE.IDLE;
    }
    private void FixedUpdate()
    {
        if (CurrentGravity * (transform.position.y - startHeight) >= currentJumpHeight) //점프높이 도달시 하강
        {
            Landing();
            startHeight = transform.position.y;
        }
        if (rb.velocity.y * CurrentGravity + flyingTime * CurrentGravity < 0f)        //떨어질때 속도 보정
        {
            playerState.FallingState();
            rb.velocity = new Vector2(0f, currentLandingSpeed * CurrentGravity);
        }
        else
            playerState.IdleState();
    }
    private void Update()
    {
        if (isNuckBack)
            return;

        MoveLeftRight();// 좌우 이동
        ClimbLadder(); // 사다리타기
        Jump(); // 점프
    }

    private void MoveLeftRight()
    {

        if (isIce)
            IceMove();
        else
            NormalMove();

    }

    private void NormalMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (isWall(Vector3.left))
                return;
            playerState.Moving(false);
            transform.position += Vector3.left * currentSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (isWall(Vector3.right))
                return;
            playerState.Moving(true);
            transform.position += Vector3.right * currentSpeed * Time.deltaTime;
        }
    }

    private void IceMove() // 빙판위에서의 움직임
    {

        if (iceMoveForce.y != 0) // 지면에서만 적용
        {
            return;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (isWall(Vector3.left)) // 벽체크
                return;
            playerState.Moving(false);

            iceMoveForce -= new Vector2(iceMoveSpeed, 0f); // 미끄러지는 방향으로 미끄러지는 정도를 누적시킨다
            if (iceMoveForce.x < -maxIceMoveSpeed)  // 최대 속력제한
                iceMoveForce.x = -maxIceMoveSpeed;
        }
        else if (Input.GetKey(KeyCode.D)) // 반대편도 마찬가지
        {
            if (isWall(Vector3.right))
                return;
            playerState.Moving(true);
            iceMoveForce += new Vector2(iceMoveSpeed, 0f);
            if (iceMoveForce.x > maxIceMoveSpeed)
                iceMoveForce.x = maxIceMoveSpeed;
        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) // 둘 다 입력이 없으면 미끄러짐 적용
        {
            if (isWall(Vector3.left) || isWall(Vector3.right))
            {
                iceMoveForce.x = 0f; // 벽이 있으면 미끄러짐 멈춤
                return;
            }

            float frictionForce = Mathf.Abs(iceMoveForce.x) * friction; // 설정된 마찰력만큼 미끄러짐 감소
            if (iceMoveForce.x < 0f)
            {
                iceMoveForce.x += frictionForce; // 감소된 힘 적용
                if (iceMoveForce.x > 0f)
                    iceMoveForce.x = 0f;
            }
            else if (iceMoveForce.x > 0f) // 반대편도 마찬가지
            {
                iceMoveForce.x -= frictionForce;
                if (iceMoveForce.x < 0f)
                    iceMoveForce.x = 0;
            }
        }
        transform.position += new Vector3(iceMoveForce.x, iceMoveForce.y, 0f); //최종적인 포지션 업데이트
    }

    private void ClimbLadder()
    {
        if (rb.bodyType == RigidbodyType2D.Kinematic)
            playerState.ClimbingState();
        // 사다리 올라가기
        if (isLadder == true && Input.GetKey(KeyCode.W))
        {
            if (CanClimb(Vector2.up) == false) // 올라갈 수 있는 상황인지 확인
            {
                rb.bodyType = RigidbodyType2D.Dynamic; // 없다면 리지드바디를 다이내믹으로 바꿔서 물리효과 적용되게 함
                SetGravity(groundGravity); // 원래의 중력으로 되돌리기
                return;
            }
            playerState.ClimbingState();
            SetGravity(1f);
            rb.velocity = Vector3.zero;
            rb.bodyType = RigidbodyType2D.Kinematic; // 사다리를 탈 때는 중력과 사다리에 연결된 사물에 물리 영향을 받지 않는다
            transform.position += Vector3.up * currentSpeed * Time.deltaTime;
        }
        // 사다리 내려오기
        else if (isLadder == true && Input.GetKey(KeyCode.S))
        {
            if (CanClimb(Vector2.down) == false) // 아래로 가는 것도 위와 마찬가지
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                SetGravity(groundGravity);
                return;
            }
            playerState.ClimbingState();
            SetGravity(1f);
            rb.velocity = Vector3.zero;
            Debug.Log("RBG : " + rb.gravityScale);
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.position += Vector3.down * currentSpeed * Time.deltaTime;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            startHeight = transform.position.y; //점프 시작위치 저장
            playerState.FlyingState();
            isJump = true; // 점프 상태로 변경합니다.
            rb.AddForce(Vector2.up * CurrentGravity * currentJumpPower, ForceMode2D.Impulse);
        }
    }
    private void Landing() // 착지시점이 되면 리지드바디의 벨로시티의 y값을 바꾼다
    {
        rb.velocity = new Vector2(0f, -1f) * CurrentGravity;

    }
    private bool CanClimb(Vector2 _dir) // 사다리를 내려갈때는 머리위에다가 레이를 쏴서 사다리를 다 내려왔는지 체크하고
    {                                   // 올라갈때는 발밑에다가 레이를 쏴서 사다리를 다 올라왔는지 체크한다
        float head = col.bounds.max.y;
        float foot = col.bounds.min.y;
        Vector2 dir;
        float y;

        if (_dir == Vector2.up)
        {
            dir = Vector2.up;
            y = foot;
        }
        else
        {
            dir = Vector2.down;
            y = head;
        }

        RaycastHit2D rayHit = Physics2D.Raycast(new Vector2(rb.position.x, y), dir, 15.0f, LayerMask.GetMask("Ladder"));
        Debug.DrawRay(new Vector2(rb.position.x, y), dir);

        return rayHit.collider != null;
    }
    private bool CanJump() // 왼발 오른발에서 레이를 쏴서 점프가능한 상황 체크 
    {                      // 블록끝에 걸터있을때 충돌체가 인식이 안돼서 점프가 가능한 상황임에도 못하는 것으로 판정하는 상황을 개선하고자 사용된 함수     
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

        Debug.DrawRay(rb.position + new Vector2(-(transform.localScale.x * 0.4f), footY), rayDir);

        return (rayHitRight.collider != null || rayHitLeft.collider != null);
    }
    private bool isWall(Vector3 _dir) //머리 가슴 발에서 레이를 쏴서 벽이 있는지 체크 벽을 못뚫게 하려고 방지하는 함수
    {
        float gravityDir = CurrentGravity > 0 ? CurrentGravity : -CurrentGravity; // 일반중력 ? 일반중력 : -일반중력 일반중력이 아닐경우 머리 발 역수로 변경
        float head = col.bounds.max.y - gravityDir * 0.1f;
        float foot = col.bounds.min.y + gravityDir * 0.1f;

        RaycastHit2D hitMid = Physics2D.Raycast(rb.position, _dir, 0.5f, LayerMask.GetMask("Ground"));
        RaycastHit2D hitHead = Physics2D.Raycast(new Vector2(rb.position.x, head), _dir, 0.5f, LayerMask.GetMask("Ground"));
        RaycastHit2D hitfoot = Physics2D.Raycast(new Vector2(rb.position.x, foot), _dir, 0.5f, LayerMask.GetMask("Ground"));

        return (hitMid.collider != null || hitHead.collider != null || hitfoot.collider != null);
    }
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Debug.Log("Col : " + _collision.gameObject.tag);

        // 땅에 닿았을 때 점프 상태를 해제합니다.
        if (_collision.gameObject.layer == 3)
        {
            if (_collision.gameObject.CompareTag("Ice"))
                isIce = true;
            //else
            //    isIce = false;

            if (isJump)
            {
                playerState.IdleState();
                state = STATE.IDLE;
                isJump = false;
                return;
            }
            if (state == STATE.CLIMB) // 사다리를 올라가는데 윗지면에 부딪혀도 올라가야하므로 물리연산을 무시하게 한다
                rb.bodyType = RigidbodyType2D.Kinematic;

            playerState.IdleState();
            state = STATE.IDLE;
        }
        else if (_collision.gameObject.CompareTag("Monster"))
            Nuckback(_collision.gameObject.transform.position); // 몬스터와 충돌시 넉백

        if (_collision.gameObject.layer == 6)
            Die();

    }
    private void OnCollisionExit2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Ice") && !isJump)
        {
            isIce = false;
            iceMoveForce = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        // 사다리에 닿았을 때 사다리 상태를 활성화합니다.
        if (_collision.CompareTag("Ladder"))
        {
            isLadder = true;

        }
        else if (_collision.gameObject.CompareTag("Thron"))
        {
            isNuckBack = true;
            Die();
        }
    }
    private void OnTriggerExit2D(Collider2D _collision)
    {
        // 사다리에서 벗어났을 때 사다리 상태를 비활성화합니다.
        if (_collision.CompareTag("Ladder"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            playerState.IdleState();
            state = STATE.IDLE;
            isLadder = false;
            Debug.Log("Goundg : " + groundGravity);
            SetGravity(groundGravity);
        }
    }

    public void Die() // 사망
    {
        playerState.DieState();
        isNuckBack = true;
        dieUI.gameObject.SetActive(true);
    }

    private IEnumerator DamageEffect()
    {
        Vector3 curScale = transform.localScale;
        transform.localScale = curScale * 1.2f;         // 타격감효과를 위해 일시적으로 크기키우기
        spriteRenderer.color = new UnityEngine.Color(1f, 0f, 0f, 0.5f); // 알파값과 rgb값을 조정해 반짝이는 효과
        yield return new WaitForSecondsRealtime(0.02f);
        spriteRenderer.color = new UnityEngine.Color(1f, 0f, 0f, 1f);
        yield return new WaitForSecondsRealtime(0.05f);
        spriteRenderer.color = new UnityEngine.Color(1f, 1f, 1f, 1f);
        yield return new WaitForSecondsRealtime(0.3f);
        transform.localScale = curScale;
        rb.velocity = Vector3.zero;
        isNuckBack = false;
        if (--hp <= 0)
            Die();
    }

    public void Nuckback(Vector3 _attackPos)
    {
        if (isNuckBack)
            return;

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
        base.ZeroGravity();
        groundGravity = rb.gravityScale;
        col.offset = new Vector2(0f, -0.24f);
    }

    override public void NormalGravity()
    {
        base.NormalGravity();
        groundGravity = rb.gravityScale;
        col.offset = new Vector2(0f, -0.24f);
    }
    override public void CounterGravity()
    {
        base.CounterGravity();
        groundGravity = rb.gravityScale;
        col.offset = new Vector2(0f, 0.24f);
    }

    override public void UpSizing()
    {
        base.UpSizing();
        rb.drag = 8f;
    }

}