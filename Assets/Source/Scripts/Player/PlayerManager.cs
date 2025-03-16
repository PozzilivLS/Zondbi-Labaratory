using UnityEngine;
using StateMachine;
using Additions;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private FinalStateMachine _stateMachine;
        
        public PlayerMovementState MovementState;
        public PlayerIdleState IdleState;
        public PlayerWorkState WorkState;

        private void Start()
        {
            _stateMachine = new FinalStateMachine();
            MovementState = new PlayerMovementState(this);
            IdleState = new PlayerIdleState(this);
            WorkState = new PlayerWorkState(this);

            _stateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            _stateMachine.CurrentState.HandleInput();
            _stateMachine.CurrentState.LogicUpdate();
            _stateMachine.CurrentState.TransitionCheck();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
