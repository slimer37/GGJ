using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    [SerializeField] Transform _cam;
    [SerializeField] float _sensitivity;
    [SerializeField] float _clamp;
    [SerializeField] float _speed;
    [SerializeField] float _gravity;
    [SerializeField] float _smoothing;
    [SerializeField] Animator _animator;

    float _camEuler;

    float _yVelocity;

    Vector2 _smoothInput;

    Vector2 _inputSmoothingVelocity;

    Controls _controls;

    public bool CanMove { get; private set; } = true;

    static readonly int SpeedXId = Animator.StringToHash("SpeedX");
    static readonly int SpeedYId = Animator.StringToHash("SpeedY");

    void Awake()
    {
        _controls = new Controls();

        _controls.Enable();

        _controls.Player.Look.performed += Look;

        _camEuler = _cam.localEulerAngles.x;
    }

    void OnDisable()
    {
        _smoothInput = Vector2.zero;
    }

    private void Look(InputAction.CallbackContext obj)
    {
        var delta = _sensitivity * Time.deltaTime * obj.ReadValue<Vector2>();

        _camEuler -= delta.y;

        _camEuler = Mathf.Clamp(_camEuler, -_clamp, _clamp);

        transform.Rotate(Vector3.up * delta.x);

        _cam.localEulerAngles = Vector3.right * _camEuler;
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
            _yVelocity = 0;
        }
        else
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }

        var input = _controls.Player.Move.ReadValue<Vector2>();

        _smoothInput = Vector2.SmoothDamp(_smoothInput, input, ref _inputSmoothingVelocity, _smoothing);

        var movement = _speed * Time.deltaTime * new Vector3(_smoothInput.x, _yVelocity, _smoothInput.y);
        _controller.Move(transform.TransformDirection(movement));

        _animator.SetFloat(SpeedXId, _smoothInput.x);
        _animator.SetFloat(SpeedYId, _smoothInput.y);
    }
}
