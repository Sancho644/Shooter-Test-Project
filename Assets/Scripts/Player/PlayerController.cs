using Components;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Movement")] 
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 3f;
        [SerializeField] private float _gravity = 3f;
        [SerializeField] private CharacterController _controller;

        [Space] [Header("Player Shoot")] 
        [SerializeField] private float _raycastDistance = 999f;
        [SerializeField] private Transform _spawnBulletPosition;
        [SerializeField] private Transform _bulletPrefab;
        [SerializeField] private AmmoCount _ammoCount;
        [SerializeField] private LayerMask _aimColliderLayer;

        private Vector3 _playerVelocity;
        private Vector3 _mouseWorldPosition;
        private bool _isGrounded;
        private Camera _camera;

        private const float THRESHOLD = -3f;

        private void Start()
        {
            _camera = Camera.main;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            _isGrounded = _controller.isGrounded;

            _mouseWorldPosition = Vector3.zero;

            var screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            var ray = _camera.ScreenPointToRay(screenCenterPoint);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, _raycastDistance, _aimColliderLayer))
                _mouseWorldPosition = raycastHit.point;
        }

        public void PlayerMovement(Vector2 input)
        {
            var moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            _controller.Move(transform.TransformDirection(moveDirection) * _speed * Time.deltaTime);
            _playerVelocity.y += _gravity * Time.deltaTime;

            if (_isGrounded && _playerVelocity.y < 0)
                _playerVelocity.y = -2f;

            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        public void PlayerJump()
        {
            if (_isGrounded)
                _playerVelocity.y += Mathf.Sqrt(_jumpForce * THRESHOLD * _gravity);
        }

        public void PlayerShoot()
        {
            var aimDir = (_mouseWorldPosition - _spawnBulletPosition.position).normalized;
            Instantiate(_bulletPrefab, _spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));

            _ammoCount.SetAmmoCount(1);
        }
    }
}