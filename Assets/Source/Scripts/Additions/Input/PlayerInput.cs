using UnityEngine;
using UnityEngine.InputSystem;

namespace Additions
{
    public class PlayerInput : MonoBehaviour
    {
        private InputSystem_Actions _inputActions;
        private Vector2 _moveDirection;

        private bool _isInteract;
        private bool _isPaused;
        private bool _isDrag;

        public event Event GrabStateChanged;
        public event Event<bool> PauseStateChanged;

        public bool IsInteract => _isInteract;
        public bool IsPaused => _isPaused;
        public Vector2 MoveDirection => _moveDirection;

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
            _moveDirection = new Vector2();
            _inputActions.Enable();
        }

        private void OnEnable()
        {
            _inputActions.Player.Move.performed += OnPlayerMove;
            _inputActions.Player.Move.canceled += OnPlayerMove;

            _inputActions.Player.Interact.started += OnPlayerBeginInteract;
            _inputActions.Player.Interact.canceled += OnPlayerStopInteract;

            _inputActions.Player.DragObject.started += OnPlayerDragObject;

            _inputActions.Player.MenuInGame.started += OnPlayerInteractWithMenu;
        }

        private void OnPlayerDragObject(InputAction.CallbackContext context)
        {
            GrabStateChanged?.Invoke();
        }

        private void OnPlayerBeginInteract(InputAction.CallbackContext context)
        {
            _isInteract = true;
        }

        private void OnPlayerStopInteract(InputAction.CallbackContext context)
        {
            _isInteract = false;
        }

        private void OnPlayerMove(InputAction.CallbackContext context)
        {
            _moveDirection = _inputActions.Player.Move.ReadValue<Vector2>();
        }

        private void OnPlayerInteractWithMenu(InputAction.CallbackContext context) 
        {
            _isPaused = !_isPaused;
            PauseStateChanged?.Invoke(_isPaused);
        }

        private void OnDisable()
        {
            _inputActions.Player.Move.performed -= OnPlayerMove;
            _inputActions.Player.Move.canceled -= OnPlayerMove;

            _inputActions.Player.Interact.started -= OnPlayerBeginInteract;
            _inputActions.Player.Interact.canceled -= OnPlayerStopInteract;

            _inputActions.Player.DragObject.started -= OnPlayerDragObject;
        }

        private void OnDestroy()
        {
            _inputActions.Disable();
        }
    }
}