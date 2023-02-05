using System;
using System.Collections;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] string _hideState;
    [SerializeField] string _emergeState;
    [SerializeField] Movement _movement;
    [SerializeField] AudioClip _emergeClip;
    [SerializeField] AudioClip _hideClip;
    [SerializeField] AudioSource _source;
    [SerializeField] float _volumeScale;

    [Header("Wheat Check")]
    [SerializeField] Transform _wheatCheck;
    [SerializeField] float _wheatCheckRadius;
    [SerializeField] LayerMask _wheat;
    [SerializeField] GameObject _exposedMessage;
    [SerializeField] GameObject _hidingMessage;
    [SerializeField] GameObject _firstRootMessage;

    public event Action<bool> OnRoot;

    int _hideStateId;
    int _emergeStateId;

    Controls _controls;

    bool _isRooted;

    bool _firstRoot = true;

    bool _isAnimating;

    public bool _isHiding;

    void Awake()
    {
        _hideStateId = Animator.StringToHash(_hideState);
        _emergeStateId = Animator.StringToHash(_emergeState);

        _controls = new Controls();

        _controls.Enable();

        _controls.Player.TakeRoot.performed += _ => StartTakingRoot();

        StartTakingRoot();

        _firstRootMessage.SetActive(true);
        _exposedMessage.SetActive(false);
        _hidingMessage.SetActive(false);
    }

    public void StartTakingRoot()
    {
        if (_isAnimating) return;

        StartCoroutine(TakeRoot());
    }

    IEnumerator TakeRoot()
    {
        _isAnimating = true;

        _isRooted = !_isRooted;

        if (_isRooted)
        {
            _animator.Play(_hideStateId, 0);
            _movement.enabled = false;
            _source.PlayOneShot(_hideClip, _volumeScale);
        }
        else
        {
            _animator.Play(_emergeStateId, 0);
            _source.PlayOneShot(_emergeClip, _volumeScale);

            OnRoot?.Invoke(false);

            if (_firstRoot)
            {
                _firstRootMessage.SetActive(false);
                _firstRoot = false;
            }
        }

        yield return null;

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        if (_isRooted)
        {
            OnRoot?.Invoke(true);
        }
        else
        {
            _movement.enabled = true;
        }

        _isAnimating = false;
    }

    void FixedUpdate()
    {
        if (_firstRoot) return;

        if (!_isRooted || _isAnimating)
        {
            _isHiding = false;
            _exposedMessage.SetActive(false);
            _hidingMessage.SetActive(false);
            return;
        }

        _isHiding = Physics.CheckSphere(_wheatCheck.position, _wheatCheckRadius, _wheat, QueryTriggerInteraction.Collide);

        _exposedMessage.SetActive(!_isHiding);
        _hidingMessage.SetActive(_isHiding);
    }
}
