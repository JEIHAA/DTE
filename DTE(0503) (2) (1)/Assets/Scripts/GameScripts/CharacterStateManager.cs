using UnityEngine;

namespace Character
{
    public interface ICharacterState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
    }

    public class CharacterStateManager : MonoBehaviour
    {
        protected ICharacterState curState;
        protected ICharacterState prevState;

        public void ChangeState(ICharacterState _nextState)
        {

            if (_nextState == curState)
            {
                return;
            }

            if (curState != null)
            {
                curState.ExitState();
            }

            prevState = curState;
            curState = _nextState;
            curState.EnterState();
        }

        public void Update()
        {
            if (curState != null)
                curState.UpdateState();
        }

        public ICharacterState GetPreviousState()
        {
            return prevState;
        }
    }

    public class IdleState : ICharacterState
    {
        private PlayerState player;
        private MonsterState monster;
        private Animator animator;

        public IdleState(PlayerState _player, Animator _animator)
        {
            this.player = _player;
            this.animator = _animator;
        }
        public IdleState(MonsterState _monster, Animator _animator)
        {
            this.monster = _monster;
            this.animator = _animator;
        }

        public void EnterState()
        {
            if (animator != null)
                animator.SetBool("isIdle", true);
        }

        public void ExitState()
        {
            if(animator != null)
            animator.SetBool("isIdle", false);
        }

        public void UpdateState()
        {
        }
    }

    public class MovingState : ICharacterState
    {
        private PlayerState player;
        private MonsterState monster;
        private Animator animator;
        private SpriteRenderer renderer;
        private bool dir;

        public MovingState(PlayerState _player, Animator _animator, SpriteRenderer _renderer, bool _direction)
        {
            this.player = _player;
            this.animator = _animator;
            this.renderer = _renderer;
            dir = _direction;
        }

        public MovingState(MonsterState _monster, Animator _animator)
        {
            this.monster = _monster;
            this.animator = _animator;
        }

        public void EnterState()
        {
            animator.SetBool("isMoving", true);
            if (monster != null) return;

            renderer.flipX = dir;
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
            ICharacterState prevState = player.GetPreviousState();
            animator.SetBool("isJumping", true);
        }

        public void ExitState()
        {
            animator.SetBool("isJumping", false);
        }

        public void UpdateState()
        {

        }
    }

    public class DieState : ICharacterState
    {
        private PlayerState player;
        private MonsterState monster;
        private Animator animator;
        private AudioSource audioSource;
        public DieState(PlayerState _player, Animator _animator)
        {
            this.player = _player;
            this.animator = _animator;
            Debug.Log("YOU DDDDDDDDDDIIIIIIEEEEEEE");
        }
        public DieState(MonsterState _monster, Animator _animator) { 
            this.monster = _monster;
            this.animator = _animator;
            Debug.Log($"{monster.gameObject.name} DDDDDDDDDDIIIIIIEEEEEEE");
        }

        public void EnterState()
        {
            animator.SetBool("isDie", true);
        }
        public void ExitState()
        {
        }
        public void UpdateState()
        {
        }
    }
}
