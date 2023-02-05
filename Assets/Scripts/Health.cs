using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] CanvasGroup _group;
    [SerializeField] float _fadeTime;
    [SerializeField] float _maxHealth;
    [SerializeField] float _delay;
    [SerializeField] Root _root;
    [SerializeField] float _rootHeal;
    [SerializeField] CanvasGroup _deathScreen;
    [SerializeField] Animator animator;
    [SerializeField] float _deathScreenDelay;
    [SerializeField] float _deathFadeTime;
    [SerializeField] Movement _movement;
    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip[] _hurtClips;
    [SerializeField] float _volumeScale;

    float _health;

    float _timeSinceHealthChange;

    Tween _tween;

    bool _isRooted;

    public static Health Instance { get; private set; }

    public static bool IsDead { get; private set; }

    void Awake()
    {
        Instance = this;

        IsDead = false;

        _health = _maxHealth;
        _root.OnRoot += OnRoot;

        _deathScreen.alpha = 0;
        _deathScreen.interactable = false;
    }

    public void InstaKill()
    {
        Damage(_maxHealth);
    }

    private void OnRoot(bool isRooted)
    {
        _isRooted = isRooted;

        UpdateHealth();
    }

    public void Damage(float damage)
    {
        _health -= damage;

        if (!UpdateHealth()) return;

        _source.PlayOneShot(_hurtClips[Random.Range(0, _hurtClips.Length)], _volumeScale);
    }

    bool UpdateHealth()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        if (_slider.value == _health / _maxHealth) return false;

        _slider.value = _health / _maxHealth;

        _group.DOKill();
        _group.alpha = 1;

        _timeSinceHealthChange = 0;

        if (_health <= 0)
        {
            StartCoroutine(Die());
        }

        return true;
    }

    void Update()
    {
        if (IsDead) return;

        if (_isRooted)
        {
            _health = Mathf.Clamp(_health + _rootHeal * Time.deltaTime, 0, _maxHealth);
            _slider.value = _health / _maxHealth;

            return;
        }

        _timeSinceHealthChange += Time.deltaTime;

        if (!_tween.IsActive() && _timeSinceHealthChange > _delay)
        {
            _tween = _group.DOFade(0, _fadeTime);
        }
    }

    IEnumerator Die()
    {
        if (IsDead) yield break;

        _movement.enabled = false;

        IsDead = true;

        animator.Play("die", 0);

        yield return new WaitForSeconds(_deathScreenDelay);

        _deathScreen.DOFade(1, _deathFadeTime);
        _deathScreen.interactable = true;

        Destroy(_movement);
    }
}
