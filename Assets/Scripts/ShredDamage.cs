using UnityEngine;

public class ShredDamage : MonoBehaviour
{
    [SerializeField] float _damageInterval;
    [SerializeField] float _damage;
    [SerializeField] Health _health;

    float _interval;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Kill")) return;

        if (Root.Instance.IsRooted)
        {
            _health.InstaKill();
            return;
        }

        _interval += Time.fixedDeltaTime;

        if (_interval > _damageInterval)
        {
            _health.Damage(_damage);
            _interval = 0;
        }
    }
}
