using _project.Scripts.HeroLogic.Animators;
using _project.Scripts.Services.Input;
using UnityEngine;

namespace _project.Scripts.HeroLogic
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private HeroAnimatorController _heroAnimatorController;

        [SerializeField] private ArmsAnimatorController _armsAnimatorController;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private InputService _inputService;
        private CameraComponent _cameraComponent;

        public InputService InputService => _inputService;
        public bool IsInitialized { get; private set; }


        public void Initialize()
        {
            _inputService = GetComponent<InputService>();
            _cameraComponent = GetComponent<CameraComponent>();
            _moveComponent.Initialize(_cameraComponent, _heroAnimatorController, _armsAnimatorController);
            _inputService.Initialize(_moveComponent, _cameraComponent);
            _cameraComponent.SetTarget(transform);
            
            IsInitialized = true;
        }

    }
}