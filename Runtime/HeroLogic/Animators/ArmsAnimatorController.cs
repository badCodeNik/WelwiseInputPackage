using UnityEngine;

namespace _project.Scripts.HeroLogic.Animators
{
    public class ArmsAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _idleAnimationDelay = 8f;
        private readonly int _isRunningToHash = Animator.StringToHash("isRunning");
        private readonly int _jumpToHash = Animator.StringToHash("jump");
        private float _idleTimer;
        private bool _isMoving;

        private void Awake()
        {
            _idleTimer = _idleAnimationDelay;
        }

        public void Jump()
        {
            _animator.SetTrigger(_jumpToHash);
        }

        public void Run(bool isRunning)
        {
            _isMoving = isRunning;
            _idleTimer = isRunning ? _idleAnimationDelay : _idleTimer;
            _animator.SetBool(_isRunningToHash, isRunning);
        }


        private void Update()
        {
            if (!_isMoving)
            {
                _idleTimer -= Time.deltaTime;
                if (_idleTimer <= 0)
                {
                    _idleTimer = _idleAnimationDelay;
                    _animator.SetTrigger("idle");
                }
            }
        }
    }
}