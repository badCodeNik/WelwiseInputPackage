using UnityEngine;

namespace HeroLogic
{
    public class AnimatorComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _idleAnimationDelay = 8f;
        private readonly int _isRunningToHash = Animator.StringToHash("isRunning");
        private readonly int _isFallingToHash = Animator.StringToHash("isFalling");
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

        public void Fall(bool isFalling)
        {
            _animator.SetBool(_isFallingToHash, isFalling);
        }

        private void Update()
        {
            if (!_isMoving)
            {
                _idleTimer -= Time.deltaTime;
                Debug.Log(_idleTimer);
                if (_idleTimer <= 0)
                {
                    _idleTimer = _idleAnimationDelay;
                    _animator.SetTrigger("idle");
                }
            }
        }
    }
}