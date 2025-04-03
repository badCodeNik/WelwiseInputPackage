using _project.Scripts.Services.Input;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private CustomUiButton _jumpButton;
    [SerializeField] private CustomUiButton _cameraSwitchButton;

    public Joystick Joystick => _joystick;

    public CustomUiButton JumpButton => _jumpButton;

    public CustomUiButton CameraSwitchButton => _cameraSwitchButton;
    public bool IsEnabled { get; private set; }


    public void Enable()
    {
        IsEnabled = true;
        gameObject.SetActive(true);
    }
}