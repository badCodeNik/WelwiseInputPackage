using UnityEngine;
using UnityEngine.EventSystems;

namespace Input
{
    public class CustomUiButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _isButtonDown;
        private bool _wasPressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isButtonDown = true;
            _wasPressed = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isButtonDown = false;
        }

        public bool IsPressed()
        {
            if (_isButtonDown && !_wasPressed)
            {
                _wasPressed = true;
                return true;
            }
            return false;
        }
    }
}