using Player;
using UnityEngine;

namespace Components
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _controller;
        [SerializeField] private PlayerMouseLook _look;

        private PlayerInput _playerInput;
        private PlayerInput.PlayerMovementActions _playerMovement;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerMovement = _playerInput.PlayerMovement;
            _playerMovement.Jump.performed += ctx => _controller.PlayerJump();
            _playerMovement.Shoot.performed += ctx => _controller.PlayerShoot();
        }

        private void FixedUpdate()
        {
            _controller.PlayerMovement(_playerMovement.Movement.ReadValue<Vector2>());
        }

        private void LateUpdate()
        {
            _look.MouseLook(_playerMovement.MouseLook.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
    }
}