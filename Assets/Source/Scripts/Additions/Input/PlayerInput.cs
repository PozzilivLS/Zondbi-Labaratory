using UnityEngine;
using UnityEngine.InputSystem;

namespace Additions
{
    public class PlayerInput : MonoBehaviour
    {
        private InputSystem_Actions _inputActions;
        private Vector2 _playerDirection;

        private bool _isInteract;
        private bool _isDragObject;

        public bool IsInteract => _isInteract;
        public bool IsDragObject => _isDragObject;
        public Vector2 PlayerDirection => _playerDirection;

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
            _playerDirection = new Vector2();
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
        }

        private void OnPlayerStartDragObject(InputAction.CallbackContext context)
        {
            _isDragObject = true;
        }

        private void OnPlayerStopDragObject(InputAction.CallbackContext context)
        {
            _isDragObject = false;
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
            _playerDirection = _inputActions.Player.Move.ReadValue<Vector2>();
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