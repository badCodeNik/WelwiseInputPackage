using UnityEngine;

namespace HeroLogic.Animators
{
    public class HeroAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private readonly int _isRunningToHash = Animator.StringToHash("isRunning");
        private readonly int _isFallingToHash = Animator.StringToHash("isFalling");
        private readonly int _jumpToHash = Animator.StringToHash("jump");


        public void Jump()
        {
            _animator.SetTrigger(_jumpToHash);
        }

        public void Run(bool isRunning)
        {
            _animator.SetBool(_isRunningToHash, isRunning);
        }

        public void Fall(bool isFalling)
        {
            _animator.SetBool(_isFallingToHash, isFalling);
        }
    }
}