namespace Input
{
    public interface IInputHandler
    {
        public float GetHorizontalAxis();
        public float GetVerticalAxis();
        public bool IsJump();
        public CameraInput IsCameraRotate();
        public bool SwitchCameraMode();
        public bool IsMobile();
    }
}