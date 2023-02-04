using UnityEngine;

public class CutWheat : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kill"))
        {
            Destroy(gameObject);
        }
    }
}
