using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : CharacterStateManager
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isClimbing;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeState(new PlayerIdleState(this, animator));
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Ladder"))
        {
            Debug.Log(isClimbing);
            isClimbing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.CompareTag("Ladder") && isClimbing)
        {
            isClimbing = false;
            ChangeState(new PlayerIdleState(this, animator));
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            ChangeState(new MovingState(this, animator, spriteRenderer, -1));
        else if (Input.GetKey(KeyCode.D))
            ChangeState(new MovingState(this, animator, spriteRenderer, 1));
        else if (Input.GetKey(KeyCode.W) && isClimbing || isClimbing)
            ChangeState(new ClimbingState(this, animator));
        else if (Input.GetKey(KeyCode.Space))
            ChangeState(new JumpingState(this, animator));
        else
            ChangeState(new PlayerIdleState(this, animator));

    }
}

public class PlayerIdleState : ICharacterState
{
    private PlayerState player;
    private Animator animator;

    public PlayerIdleState(PlayerState _player, Animator _animator)
    {
        this.player = _player;
        this.animator = _animator;
    }

    public void EnterState()
    {
        animator.SetBool("isIdle", true);
    }

    public void ExitState()
    {
        animator.SetBool("isIdle", false);
    }

    public void UpdateState()
    {
    }
}

public class MovingState : ICharacterState
{
    private PlayerState player;
    private Animator animator;
    private SpriteRenderer renderer;
    private int dir;

    public MovingState(PlayerState _player, Animator _animator, SpriteRenderer _renderer, int _direction)
    {
        this.player = _player;
        this.animator = _animator;
        this.dir = _direction;
        this.renderer = _renderer;
    }

    public void EnterState()
    {
        if (dir == -1)
            renderer.flipX = false;
        else if (dir == 1)
            renderer.flipX = true;

        animator.SetBool("isMoving", true);
    }

    public void ExitState()
    {
        animator.SetBool("isMoving", false);
    }

    public void UpdateState()
    {
    }
}

public class JumpingState : ICharacterState
{
    private PlayerState player;
    private Animator animator;

    public JumpingState(PlayerState _player, Animator _animator)
    {
        this.player = _player;
        this.animator = _animator;
    }

    public void EnterState()
    {
        Debug.Log("Player Enter jumping State");
        ICharacterState prevState = player.GetPreviousState();
        animator.SetBool("isJumping", true);
    }

    public void ExitState()
    {
        Debug.Log("Exiting jumping State");
        animator.SetBool("isJumping", false);
    }

    public void UpdateState()
    {

    }
}

public class FallingState : ICharacterState
{
    private PlayerState player;
    private Animator animator;

    public FallingState(PlayerState _player, Animator _animator)
    {
        this.player = _player;
        this.animator = _animator;
    }

    public void EnterState()
    {
        Debug.Log("Player Enter falling State");
    }

    public void ExitState()
    {
        Debug.Log("Exiting jumping State");
    }

    public void UpdateState()
    {

    }
}

public class ClimbingState : ICharacterState
{
    private PlayerState player;
    private Animator animator;

    public ClimbingState(PlayerState _player, Animator _animator)
    {
        this.player = _player;
        this.animator = _animator;
    }

    public void EnterState()
    {
        Debug.Log("Player Enter Climbing State");
        animator.SetBool("isClimbing", true);
    }

    public void ExitState()
    {
        Debug.Log("Exiting ClimbingState");
        animator.SetBool("isClimbing", false);
    }

    public void UpdateState()
    {
    }
}

