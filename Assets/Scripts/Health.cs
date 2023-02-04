using DG.Tweening;
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

    float _health;

    float _timeSinceHealthChange;

    Tween _tween;

    bool _isRooted;

    void Awake()
    {
        _health = _maxHealth;
        _root.OnRoot += OnRoot;
    }

    private void OnRoot(bool isRooted)
    {
        _isRooted = isRooted;

        UpdateHealth();
    }

    public void Damage(float damage)
    {
        _health -= damage;

        UpdateHealth();
    }

    void UpdateHealth()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        if (_slider.value == _health / _maxHealth) return;

        _slider.value = _health / _maxHealth;

        _group.DOKill();
        _group.alpha = 1;

        _timeSinceHealthChange = 0;
    }

    void Update()
    {
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
}
