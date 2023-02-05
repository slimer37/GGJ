using System.Collections;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    [SerializeField] float _gravity = 9.81f;
    [SerializeField] float _speed;
    [SerializeField] float _range;
    [SerializeField] float _cone;
    [SerializeField] float _wanderInterval;
    [SerializeField] Animator _animator;

    float _yVelocity;

    Vector3 delta;

    Coroutine _wander;

    static readonly int SpeedId = Animator.StringToHash("Speed");

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    void Awake()
    {
        _wander = StartCoroutine(Wander());
    }

    void Update()
    {
        if (!Root.Instance) return;

        if (_controller.isGrounded)
        {
            _yVelocity = 0;
        }

        _yVelocity -= _gravity * Time.deltaTime;

        var toPlayer = Root.Instance.transform.position - transform.position;

        toPlayer.Normalize();

        print(Vector3.Dot(transform.forward, toPlayer));

        if (!Root.Instance.IsHiding && toPlayer.sqrMagnitude < _range * _range && Vector3.Dot(transform.forward, toPlayer) > _cone)
        {
            delta = toPlayer;
            delta.y = 0;

            if (_wander != null)
            {
                StopCoroutine(_wander);
                _wander = null;
            }
        }
        else _wander ??= StartCoroutine(Wander());

        if (delta != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(delta);

            _controller.Move(Time.deltaTime * (Vector3.up * _yVelocity + transform.forward * _speed));

            var velocity = _controller.velocity;
            velocity.y = 0;

            _animator.SetFloat(SpeedId, velocity.sqrMagnitude);
        }
        else
        {
            _animator.SetFloat(SpeedId, 0f);
        }
    }

    IEnumerator Wander()
    {
        delta = Vector3.zero;

        while (true)
        {
            yield return new WaitForSeconds(_wanderInterval);
            var pos = Random.insideUnitCircle;
            delta = new Vector3(pos.x, 0, pos.y);
        }
    }
}
