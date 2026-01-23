using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerBaseState: BaseState
    {
        protected readonly PlayerController _playerController;
        protected readonly Animator _animator;

        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int MoveHash = Animator.StringToHash("Moving");
        protected static readonly int Ability1Hash = Animator.StringToHash("Ability1");
        protected static readonly int Ability2Hash = Animator.StringToHash("Ability2");
        protected static readonly int Ability3Hash = Animator.StringToHash("Ability3");
        protected static readonly int Ability4Hash = Animator.StringToHash("Ability4");
        protected static readonly int InteractHash = Animator.StringToHash("Interact");
        protected static readonly int AttackHash = Animator.StringToHash("Attack");
        protected static readonly int HitHash = Animator.StringToHash("Hit");
        protected static readonly int DeathHash = Animator.StringToHash("Death");
        
        protected PlayerBaseState(PlayerController playerController, Animator animator)
        {
            _playerController = playerController;
            _animator = animator;
        }
    }
    
    public enum PlayerState
    {
        Idle,
        Moving,
        Attacking,
        Ability1,
        Ability2,
        Ability3,
        Ability4,
        Interacting,
        Hit,
        Death
    }
}

