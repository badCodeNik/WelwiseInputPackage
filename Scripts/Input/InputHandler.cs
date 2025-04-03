using System;
using UnityEngine;

namespace _project.Scripts.Services.Input
{
    public class InputHandler : IDesktopInputHandler
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        public float GetHorizontalAxis()
        {
            return UnityEngine.Input.GetAxisRaw(HorizontalAxis);
        }

        public float GetVerticalAxis()
        {
            return UnityEngine.Input.GetAxisRaw(VerticalAxis);
        }

        public bool IsJump()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Space);
        }

        public CameraInput IsCameraRotate()
        {
            var cameraInfo = new CameraInput()
            {
                IsPressed = UnityEngine.Input.GetMouseButton(1),
                InputX = UnityEngine.Input.GetAxis("Mouse X"),
                InputY = -UnityEngine.Input.GetAxis("Mouse Y")
            };
            return cameraInfo;
        }

        public bool SwitchCursor()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Tab);
        }

        public bool SwitchCameraMode()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.V);
        }

        public bool IsMobile()
        {
            return false;
        }
    }
}