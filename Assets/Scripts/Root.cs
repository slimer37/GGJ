using System.Collections;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] string _hideState;
    [SerializeField] string _emergeState;
    [SerializeField] Movement _movement;

    int _hideStateId;
    int _emergeStateId;

    Controls _controls;

    bool _isRooted;

    bool _isAnimating;

    void Awake()
    {
        _hideStateId = Animator.StringToHash(_hideState);
        _emergeStateId = Animator.StringToHash(_emergeState);

        _controls = new Controls();

        _controls.Enable();

        _controls.Player.TakeRoot.performed += _ => StartTakingRoot();
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
        }
        else
        {
            _animator.Play(_emergeStateId, 0);
        }

        yield return null;

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        if (!_isRooted)
        {
            _movement.enabled = true;
        }

        _isAnimating = false;
    }
}
