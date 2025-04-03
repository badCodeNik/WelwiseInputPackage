using _project.Scripts.HeroLogic;
using UnityEngine;

namespace _project.Scripts.Services.Input
{
    public class InputService : MonoBehaviour
    {
        public bool IsEnabled { get; private set; }

        private Hud _hud;
        private IInputHandler _inputHandler;
        private ICursorHandler _cursorHandler;
        private MoveComponent _moveComponent;
        private CameraComponent _cameraComponent;
        private bool _isInitialized;

        public void Awake()
        {
            _hud = FindObjectOfType<Hud>(true);
#if UNITY_STANDALONE
            _inputHandler = new InputHandler();
            _cursorHandler = (ICursorHandler)_inputHandler;
            if (!CursorSwitcher.IsCursorEnabled)
            {
                CursorSwitcher.DisableCursor();
            }

#elif UNITY_WEBGL
            if (DeviceDetector.IsMobile())
            {
                _hud.Enable();
                _inputHandler = new MobileInputHandler(_hud);
            }
            else
            {
                _inputHandler = new InputHandler();
                _cursorHandler = (ICursorHandler)_inputHandler;
                if (!CursorSwitcher.IsCursorEnabled)
                {
                    CursorSwitcher.DisableCursor();
                }
            }
#else
            _inputHandler = new MobileInputHandler(_hud);
#endif
        }

        private void Update()
        {
            if (!_isInitialized || !IsEnabled) return;
            if (_inputHandler.IsJump())
            {
                _moveComponent.Jump();
            }

            HandleMovementInput();
            HandleCameraInput();
            if (!_inputHandler.IsMobile())
            {
                HandleCursorSwitch();
            }
        }

        private void HandleCameraInput()
        {
            var isCameraRotate = _inputHandler.IsCameraRotate();
            if (_inputHandler.SwitchCameraMode()) _cameraComponent.SwitchCameraMode();
            if (_hud.IsEnabled && _hud.Joystick.IsPointerDown) return;
            if (isCameraRotate.IsPressed && CursorSwitcher.IsCursorEnabled)
            {
                _cameraComponent.Rotate(isCameraRotate.InputX, isCameraRotate.InputY);
            }
            else if (!CursorSwitcher.IsCursorEnabled)
            {
                _cameraComponent.Rotate(isCameraRotate.InputX, isCameraRotate.InputY);
            }
        }

        private void HandleCursorSwitch()
        {
            if (_cursorHandler.SwitchCursor() && !_cameraComponent.IsFirstCamera) SwitchCursor();
        }

        private void SwitchCursor()
        {
            CursorSwitcher.SwitchCursor();
        }

        private void HandleMovementInput()
        {
            var horizontalAxis = _inputHandler.GetHorizontalAxis();
            var verticalAxis = _inputHandler.GetVerticalAxis();
            if (horizontalAxis != 0 || verticalAxis != 0)
            {
                Move(horizontalAxis, verticalAxis);
            }
            else
            {
                _moveComponent.SetDirection(Vector3.zero);
            }
        }

        public void Initialize(MoveComponent moveComponent, CameraComponent cameraComponent)
        {
            _moveComponent = moveComponent;
            _cameraComponent = cameraComponent;
            _isInitialized = true;
            IsEnabled = true;
        }

        public void DisableInput()
        {
            _moveComponent.SetDirection(Vector3.zero);
            IsEnabled = false;
        }

        private void Move(float horizontalAxis, float verticalAxis)
        {
            var direction = new Vector3(horizontalAxis, 0, verticalAxis);
            if (direction.magnitude > 0) direction.Normalize();
            _moveComponent.SetDirection(direction);
        }

        public void EnableInput()
        {
            IsEnabled = true;
        }
    }
}