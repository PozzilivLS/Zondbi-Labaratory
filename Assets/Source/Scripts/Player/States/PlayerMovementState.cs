using UnityEngine;
using StateMachine;
using Additions;

namespace Player
{
    public class PlayerMovementState : State
    {
        private PlayerManager _manager;
        private PlayerInput _input;

        private Vector2 _moveDirection;

        public PlayerMovementState(PlayerManager manager, PlayerInput input)
        {
            _manager = manager;
            _input = input;
        }

        public override void Enter()
        {
            
        }

        public override void TransitionCheck()
        {
            if (_input.MoveDirection == Vector2.zero)
            {
                _manager.ChangeState(_manager.IdleState);
            }
        }

        public override void HandleInput()
        {
            _moveDirection = _input.MoveDirection;
        }

        public override void LogicUpdate()
        {

        }

        public override void PhysicsUpdate()
        {
            _manager.Rigidbody.linearVelocity = _moveDirection * _manager.MoveSpeed;
        }

        public override void Exit()
        {

        }
    }
}
