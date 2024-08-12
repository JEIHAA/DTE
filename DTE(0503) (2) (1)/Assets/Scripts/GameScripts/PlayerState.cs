using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using TMPro;
public class PlayerState : CharacterStateManager
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    

    private bool isClimbing;
    private bool isDie = false;
    private int hp = 3;


    public void SetTool(Animator _animator, SpriteRenderer _spriteRenderer) 
    {
        animator = _animator;
        spriteRenderer = _spriteRenderer;
    }

    public void IdleState()
    {
        ChangeState(new IdleState(this, animator));
    }
    public void FlyingState()
    {
        ChangeState(new FlyingState(this, animator));
    }

    public void FallingState()
    {
        ChangeState(new FallingState(this, animator));
    }

    public void Moving(bool _dir)
    {
        ChangeState(new MovingState(this, animator, spriteRenderer, _dir));
    }

    public void ClimbingState()
    {
        ChangeState(new ClimbingState(this, animator));
    }

    public void DieState()
    {
        ChangeState(new DieState(this, animator));
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Key")) {
            ChangeState(new GetItemState(this, animator));
        }
    }
}
public class FlyingState : ICharacterState
{
    private PlayerState player;
    private Animator animator;
    public FlyingState(PlayerState _player, Animator _animator)
    {
        this.player = _player;
        this.animator = _animator;
    }
    public void EnterState()
    {
        animator.SetBool("isFly", true);
    }
    public void ExitState()
    {
        animator.SetBool("isFly", false);
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
        animator.SetBool("isFalling", true);
    }
    public void ExitState()
    {
        animator.SetBool("isFalling", false);
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
        animator.SetBool("isClimbing", true);
    }
    public void ExitState()
    {
        animator.SetBool("isClimbing", false);
    }
    public void UpdateState()
    {
    }
}
public class BlowbackState : ICharacterState
{
    private PlayerState player;
    private Animator animator;
    public BlowbackState(PlayerState _player, Animator _animator)
    {
        this.player = _player;
        this.animator = _animator;
    }
    public void EnterState()
    {
        animator.SetBool("isBlowback", true);
    }
    public void ExitState()
    {
        animator.SetBool("isBlowback", false);
    }
    public void UpdateState()
    {
    }
}

public class GetItemState : ICharacterState
{
    private PlayerState player;
    private Animator animator;
    public GetItemState(PlayerState _player, Animator _animator)
    {
        this.player = _player;
        this.animator = _animator;
    }
    public void EnterState()
    {
        animator.SetBool("GetItem", true);
    }
    public void ExitState()
    {
        animator.SetBool("GetItem", false);
    }
    public void UpdateState()
    {
    }
}