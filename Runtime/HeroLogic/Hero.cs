using HeroLogic.Animators;
using Input;
using UnityEngine;

namespace HeroLogic
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private HeroAnimatorController _heroAnimatorController;

        [SerializeField] private ArmsAnimatorController _armsAnimatorController;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private InputService _inputService;
        private CameraComponent _cameraComponent;
        private CameraObserver _cameraObserver;
        public InputService InputService => _inputService;
        public bool IsInitialized { get; private set; }


        public void Initialize()
        {
            _inputService = GetComponent<InputService>();
            _cameraComponent = GetComponent<CameraComponent>();
            _moveComponent.Initialize(_cameraComponent, _heroAnimatorController, _armsAnimatorController);
            _inputService.Initialize(_moveComponent, _cameraComponent);
            _cameraComponent.SetTarget(transform);
            _cameraObserver = new CameraObserver(_cameraComponent,
                GetComponentInChildren<SkinColorChanger>());
            
            IsInitialized = true;
        }

    }
}