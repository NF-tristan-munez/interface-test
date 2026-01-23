using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    //Handles all logic for when player goes in, out, and during idle state
    public class PlayerAbility1State: PlayerBaseState
    {
        public PlayerAbility1State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            //Use this for transitioning between different animator hashes
            _playerController.LockMovement();
            _animator.CrossFade(Ability1Hash, 0.2f);
            
            Debug.Log("Entering Player Ability1 State");
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
        }

        public override void OnExit()
        {
            _playerController.UnlockMovement();
            base.OnExit();
            Debug.Log("Ability 1 State Exited");
        }
    }
}