using UnityEngine;

public class FarmGenerator : MonoBehaviour
{
    [SerializeField] Transform[] wheats;
    [SerializeField] float unit;
    [SerializeField] int gridX;
    [SerializeField] int gridY;
    [SerializeField] float floorOffset;
    [SerializeField] float rayOffset;
    [SerializeField] Vector3 offset;
    [SerializeField] float gizmoSize;
    [SerializeField] float chanceNoWheat;
    [SerializeField] Collider ground;
    [SerializeField] Vector3 _randomOffset;

    void OnDrawGizmosSelected()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                var pos = transform.TransformPoint(new Vector3(x * unit, rayOffset, y * unit) + offset);
                ground.Raycast(new Ray(pos, Vector3.down), out var hit, 100);
                pos = hit.point + Vector3.up * floorOffset;
                Gizmos.DrawSphere(pos, gizmoSize);
            }
        }
    }

    void Awake()
    {
        Generate();
    }

    void Generate()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                if (Random.value < chanceNoWheat)
                {
                    continue;
                }

                var wheat = wheats[Random.Range(0, wheats.Length)];
                var clone = Instantiate(wheat, transform);

                var pos = transform.TransformPoint(new Vector3(x * unit, rayOffset, y * unit) + offset);
                ground.Raycast(new Ray(pos, Vector3.down), out var hit, 100);
                pos = hit.point + Vector3.up * floorOffset;

                pos += (Random.value * 2 - 1) * _randomOffset;

                clone.transform.position = pos;
            }
        }
    }
}
