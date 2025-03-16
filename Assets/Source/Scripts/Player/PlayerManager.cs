using UnityEngine;
using StateMachine;
using Additions;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _moveSpeed = 3f;

        private FinalStateMachine _stateMachine;
        
        public PlayerMovementState MovementState;
        public PlayerIdleState IdleState;
        public PlayerWorkState WorkState;

        public Rigidbody2D Rigidbody => _rigidbody2D;
        public float MoveSpeed => _moveSpeed;

        private void Start()
        {
            _stateMachine = new FinalStateMachine();
            MovementState = new PlayerMovementState(this, _input);
            IdleState = new PlayerIdleState(this, _input);
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

        internal void ChangeState(State state)
        {
            _stateMachine.ChangeState(state);
        }
    }
}
