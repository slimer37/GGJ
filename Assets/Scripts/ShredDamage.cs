using UnityEngine;

public class ShredDamage : MonoBehaviour
{
    [SerializeField] float _damageInterval;
    [SerializeField] float _farmerDamageInterval;
    [SerializeField] float _damage;
    [SerializeField] float _farmerDamage;
    [SerializeField] Health _health;

    float _interval;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Kill"))
        {
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
        else if (other.CompareTag("Farmer"))
        {
            _interval += Time.fixedDeltaTime;

            if (_interval > _farmerDamageInterval)
            {
                _health.Damage(_farmerDamage);
                _interval = 0;
            }
        }
    }
}
