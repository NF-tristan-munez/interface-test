using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    //Handles all logic for when player goes in, out, and during idle state
    public class PlayerMoveState: PlayerBaseState
    {
        public PlayerMoveState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            //Use this for transitioning between different animator hashes
            _animator.CrossFade(MoveHash, 0.2f);
            
            Debug.Log("Entering Player Move State");
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            _playerController.HandleMovement();
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting Player Move State");
        }
    }
}