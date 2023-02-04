using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction _move;
    [SerializeField] InputAction _mouseDelta;
    [SerializeField] CharacterController _controller;
    [SerializeField] Transform _cam;
    [SerializeField] float _sensitivity;
    [SerializeField] float _clamp;

    float camEuler;

    void Awake()
    {
        _move.Enable();
        _mouseDelta.Enable();

        _mouseDelta.performed += Look;

        camEuler = _cam.localEulerAngles.x;
    }

    private void Look(InputAction.CallbackContext obj)
    {
        var delta = _sensitivity * Time.deltaTime * obj.ReadValue<Vector2>();

        camEuler -= delta.y;

        camEuler = Mathf.Clamp(camEuler, -_clamp, _clamp);

        transform.Rotate(Vector3.up * delta.x);

        _cam.localEulerAngles = Vector3.right * camEuler;
    }

    void Update()
    {
        var input = _move.ReadValue<Vector2>();
        _controller.Move(transform.TransformDirection(input));
    }
}
