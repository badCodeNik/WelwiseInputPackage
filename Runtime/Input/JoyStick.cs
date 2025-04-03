using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _project.Scripts.Services.Input
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _joystickBackground;
        [SerializeField] private RectTransform _joystickHandle;
        [SerializeField] private float _handleRange = 1f;
        [SerializeField] private bool _isDynamic = true;
        [SerializeField] private float _fadeDuration = 1f;
        private Vector2 _inputVector = Vector2.zero;
        private CanvasGroup _canvasGroup;
        public bool IsPointerDown { get; private set; } = false;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            if (!_isDynamic)
            {
                _joystickBackground.gameObject.SetActive(true);
                GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                _joystickBackground.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!IsPointerDown && _canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.deltaTime / _fadeDuration;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsPointerDown = true;
            _canvasGroup.alpha = 1f;

            if (_isDynamic)
            {
                _joystickBackground.position = eventData.position;
                _joystickBackground.gameObject.SetActive(true);
            }

            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 direction = eventData.position - (Vector2)_joystickBackground.position;
            _inputVector = direction.magnitude > _joystickBackground.sizeDelta.x / 2f
                ? direction.normalized
                : direction / (_joystickBackground.sizeDelta.x / 2f);

            _joystickHandle.anchoredPosition = _inputVector * _joystickBackground.sizeDelta.x / 2f * _handleRange;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPointerDown = false;
            _inputVector = Vector2.zero;
            _joystickHandle.anchoredPosition = Vector2.zero;

            if (_isDynamic)
            {
                _joystickBackground.gameObject.SetActive(false);
            }
        }

        public Vector2 GetInputVector()
        {
            return _inputVector;
        }
    }
}