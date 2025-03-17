using UnityEngine;
using UnityEngine.InputSystem;

namespace Additions
{
    public class PlayerInput : MonoBehaviour
    {
        private InputSystem_Actions _inputActions;
        private Vector2 _moveDirection;

        private bool _isInteract;
        private bool _isDragObject;
        private bool _isPaused;

        public event Event<bool> GrabStateChanged;

        public bool IsInteract => _isInteract;
        public bool IsDragObject => _isDragObject;
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

            _inputActions.Player.DragObject.started += OnPlayerStartDragObject;
            _inputActions.Player.DragObject.canceled += OnPlayerStopDragObject;

            _inputActions.Player.MenuInGame.started += OnPlayerInteractWithMenu;
        }

        private void OnPlayerStartDragObject(InputAction.CallbackContext context)
        {
            GrabStateChanged?.Invoke(true);
        }

        private void OnPlayerStopDragObject(InputAction.CallbackContext context)
        {
            GrabStateChanged?.Invoke(false);
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
        }

        private void OnDisable()
        {
            _inputActions.Player.Move.performed -= OnPlayerMove;
            _inputActions.Player.Move.canceled -= OnPlayerMove;

            _inputActions.Player.Interact.started -= OnPlayerBeginInteract;
            _inputActions.Player.Interact.canceled -= OnPlayerStopInteract;

            _inputActions.Player.DragObject.started -= OnPlayerStartDragObject;
            _inputActions.Player.DragObject.canceled -= OnPlayerStopDragObject;
        }

        private void OnDestroy()
        {
            _inputActions.Disable();
        }
    }
}