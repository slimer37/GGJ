using System.Collections;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    [SerializeField] float _gravity = 9.81f;
    [SerializeField] float _speed;
    [SerializeField] float _wanderInterval;

    float _yVelocity;

    Vector3 delta;

    Coroutine _wander;

    void Awake()
    {
        delta = Vector3.forward;
        _wander = StartCoroutine(Wander());
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
            _yVelocity = 0;
        }

        _yVelocity -= _gravity * Time.deltaTime;

        if (!Root.Instance.IsHiding)
        {
            delta = Root.Instance.transform.position - transform.position;
            delta.y = 0;

            if (_wander != null)
            {
                StopCoroutine(_wander);
                _wander = null;
            }
        }
        else _wander ??= StartCoroutine(Wander());

        transform.rotation = Quaternion.LookRotation(delta);

        _controller.Move(Time.deltaTime * (Vector3.up * _yVelocity + transform.forward * _speed));
    }

    IEnumerator Wander()
    {
        while (true)
        {
            yield return new WaitForSeconds(_wanderInterval);
            var pos = Random.insideUnitCircle;
            delta = new Vector3(pos.x, 0, pos.y);
        }
    }
}
