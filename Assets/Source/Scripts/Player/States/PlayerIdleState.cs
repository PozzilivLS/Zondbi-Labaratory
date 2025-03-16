using Additions;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerIdleState : State
    {
        private PlayerManager _manager;
        private PlayerInput _input;

        public PlayerIdleState(PlayerManager manager, PlayerInput input)
        {
            _manager = manager;
            _input = input;
        }

        public override void Enter()
        {

        }

        public override void TransitionCheck()
        {
            if (_input.MoveDirection != Vector2.zero)
            {
                _manager.ChangeState(_manager.MovementState);
            }
        }

        public override void HandleInput()
        {

        }

        public override void LogicUpdate()
        {

        }

        public override void PhysicsUpdate()
        {
            _manager.Rigidbody.linearVelocity = Vector2.zero;
        }

        public override void Exit()
        {

        }
    }
}
