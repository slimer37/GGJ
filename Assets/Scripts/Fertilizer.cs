using UnityEngine;

public class Fertilizer : MonoBehaviour
{
    [SerializeField] float heal;
    [SerializeField] Vector3 spawnCenter;
    [SerializeField] Vector2 spawnArea;

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(spawnCenter, new Vector3(spawnArea.x, 0, spawnArea.y));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health.Instance.Heal(heal);
            Teleport();
        }
    }

    float RandomRange(float range) => (Random.value * 2 - 1) * range;

    void Teleport()
    {
        var pos = new Vector3(RandomRange(spawnArea.x / 2), 0, RandomRange(spawnArea.y / 2));

        transform.position = spawnCenter + pos;
    }
}
