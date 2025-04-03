using HeroLogic.Animators;
using UnityEngine;

namespace HeroLogic
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityForce;


        private Vector3 _lastDirection;

        private float _verticalVelocity;
        private bool _isInitialized;
        private HeroAnimatorController _heroAnimatorComp;
        private ArmsAnimatorController _armsAnimatorComp;
        private Camera _playerCamera;
        private CameraComponent _cameraComponent;

        public void SetDirection(Vector3 direction)
        {
            _lastDirection = direction;
        }


        public void Initialize(CameraComponent cameraComponent, HeroAnimatorController animatorComponent,
            ArmsAnimatorController armsAnimatorComponent)
        {
            _cameraComponent = cameraComponent;
            _heroAnimatorComp = animatorComponent;
            _armsAnimatorComp = armsAnimatorComponent;
            _playerCamera = Camera.main;
            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized) return;
            HandleGravity();
            Move(_lastDirection);

            if (!_characterController.isGrounded && _verticalVelocity < 0)
            {
                _heroAnimatorComp.Fall(true);
            }
            else
            {
                _heroAnimatorComp.Fall(false);
            }
        }

        private void HandleGravity()
        {
            _verticalVelocity += _gravityForce * Time.deltaTime;
        }


        private void Move(Vector3 direction)
        {
            _heroAnimatorComp.Run(direction.magnitude != 0);
            _armsAnimatorComp.Run(direction.magnitude != 0);
            var cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z)
                .normalized;
            var cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z)
                .normalized;
            var movementDirection = cameraRightXZ * direction.x + cameraForwardXZ * direction.z;
            var movementDelta = movementDirection * _moveSpeed * Time.deltaTime;
            Rotate(movementDelta);
            var finalMovement = new Vector3(movementDelta.x, _verticalVelocity * Time.deltaTime, movementDelta.z);
            _characterController.Move(finalMovement);
        }


        public void Rotate(Vector3 direction)
        {
            if (direction.magnitude > 0)
            {
                var targetRotation = Quaternion.LookRotation(direction);
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }


        public void Jump()
        {
            if (CanJump())
            {
                if (_cameraComponent.IsFirstCamera)
                {
                    _armsAnimatorComp.Jump();
                }
                else
                {
                    _heroAnimatorComp.Jump();
                }

                _verticalVelocity = Mathf.Sqrt(_jumpForce * -2f * _gravityForce);
            }
        }

        private bool CanJump()
        {
            return _characterController.isGrounded;
        }
    }
}