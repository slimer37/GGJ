using UnityEngine;

public class Fertilizer : MonoBehaviour
{
    [SerializeField] float points;
    [SerializeField] float heal;
    [SerializeField] float floatAmplitude;
    [SerializeField] float floatFrequency;
    [SerializeField] Vector3 spawnCenter;
    [SerializeField] Vector2 spawnArea;

    Vector3 basePosition;

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(spawnCenter, new Vector3(spawnArea.x, 0, spawnArea.y));
    }

    void Awake()
    {
        Teleport();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Consume();
        }
    }

    void Consume()
    {
        Health.Instance.Heal(heal);
        Teleport();
        Score.Instance.Value += points;
    }

    void Update()
    {
        transform.position = basePosition + Vector3.up * (Mathf.Sin(Time.time * floatFrequency) * floatAmplitude + 1);
    }

    float RandomRange(float range) => (Random.value * 2 - 1) * range;

    void Teleport()
    {
        var pos = new Vector3(RandomRange(spawnArea.x / 2), 0, RandomRange(spawnArea.y / 2));

        basePosition = spawnCenter + pos;

        transform.SetPositionAndRotation(basePosition, Quaternion.Euler(Vector3.up * Random.value * 360));
    }
}
