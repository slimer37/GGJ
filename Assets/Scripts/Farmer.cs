using UnityEngine;

public class Farmer : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    [SerializeField] float _gravity = 9.81f;
    [SerializeField] float _speed;

    static Root _player;

    float _yVelocity;

    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Root>();
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
            _yVelocity = 0;
        }

        _yVelocity -= _gravity * Time.deltaTime;

        if (_player.isHiding) return;

        var delta = _player.transform.position - transform.position;

        delta.Normalize();

        delta.y = _yVelocity;

        _controller.Move(_speed * Time.deltaTime * delta);
    }
}
