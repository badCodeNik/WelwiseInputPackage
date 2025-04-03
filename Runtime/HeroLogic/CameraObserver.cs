namespace HeroLogic
{
    public class CameraObserver
    {
        private readonly CameraComponent _cameraComponent;
        private readonly SkinColorChanger _skinColorChanger;


        public CameraObserver(CameraComponent cameraComponent, SkinColorChanger skinColorChanger)
        {
            _cameraComponent = cameraComponent;
            _skinColorChanger = skinColorChanger;
            _cameraComponent.OnCameraModeChanged += CameraModeChange;
        }


        private void CameraModeChange(bool isFirstCamera)
        {
            _skinColorChanger.SwitchBody(isFirstCamera);
            _skinColorChanger.SwitchArms(isFirstCamera);
        }

        public void Update()
        {
            _skinColorChanger.SetSkinFade(_cameraComponent.CameraDistance);
        }
    }
}