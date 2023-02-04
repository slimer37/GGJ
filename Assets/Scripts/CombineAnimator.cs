using DG.Tweening;
using UnityEngine;

public class CombineAnimator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] Transform _cylinder;
    [SerializeField] Transform[] rakes;

    [Header("Shaking")]
    [SerializeField] Transform _body;
    [SerializeField] float _shakeStrength;
    [SerializeField] int _vibrato;

    [Header("Driving")]
    [SerializeField] float _speed;
    [SerializeField] Vector3 _direction;
    [SerializeField] Transform[] wheels;
    [SerializeField] float _wheelSpinSpeed;
    [SerializeField] Vector3 _wheelDirection;

    void Awake()
    {
        _body.DOShakeRotation(5, _shakeStrength, _vibrato, 90, false, ShakeRandomnessMode.Harmonic).SetLoops(-1);
    }

    void Update()
    {
        var rot = _rotationSpeed * Time.deltaTime;
        _cylinder.Rotate(Vector3.right * rot);

        foreach (var rake in rakes)
        {
            rake.Rotate(Vector3.right * -rot);
        }

        foreach (var wheel in wheels)
        {
            wheel.Rotate(-_wheelSpinSpeed * Time.deltaTime * _wheelDirection);
        }

        transform.Translate(_speed * Time.deltaTime * _direction);
    }
}