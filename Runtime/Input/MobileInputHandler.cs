using UnityEngine;

namespace Input
{
    public class MobileInputHandler : IMobileInputHandler
    {
        private readonly Hud _hud;


        public MobileInputHandler(Hud hud)
        {
            _hud = hud;
        }

        public float GetHorizontalAxis()
        {
            return _hud.Joystick.GetInputVector().x;
        }

        public float GetVerticalAxis()
        {
            return _hud.Joystick.GetInputVector().y;
        }

        public bool IsJump()
        {
            return _hud.JumpButton.IsPressed();
        }

        public CameraInput IsCameraRotate()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                var touch = UnityEngine.Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    float sensitivity = 0.1f;
                    var touchInfo = new CameraInput()
                    {
                        IsPressed = true,
                        InputX = -touch.deltaPosition.x * sensitivity,
                        InputY = touch.deltaPosition.y * sensitivity
                    };
                    return touchInfo;
                }
            }

            return new CameraInput();
        }


        public bool SwitchCameraMode()
        {
            return _hud.CameraSwitchButton.IsPressed();
        }

        public bool IsMobile()
        {
            return true;
        }
    }

    public struct CameraInput
    {
        public float InputX;
        public float InputY;
        public bool IsPressed;
    }
}