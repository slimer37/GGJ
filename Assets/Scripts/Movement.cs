using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    [SerializeField] Transform _cam;
    [SerializeField] float _sensitivity;
    [SerializeField] float _clamp;
    [SerializeField] float _speed;
    [SerializeField] Animator _animator;

    float _camEuler;

    Controls _controls;

    static readonly int SpeedId = Animator.StringToHash("Speed");

    void Awake()
    {
        _controls = new Controls();

        _controls.Enable();

        _controls.Player.Look.performed += Look;

        _camEuler = _cam.localEulerAngles.x;
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
        var input = _controls.Player.Move.ReadValue<Vector2>();
        var movement = new Vector3(input.x, 0, input.y) * _speed * Time.deltaTime;
        _controller.Move(transform.TransformDirection(movement));

        _animator.SetFloat(SpeedId, input.sqrMagnitude);
    }
}
