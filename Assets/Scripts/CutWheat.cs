using UnityEngine;

public class CutWheat : MonoBehaviour
{
    Collider col;

    float time;

    const float GrowTime = 30;
    const float GrowDelay = 10;
    const float GrownThreshold = 25;

    void Awake()
    {
        col = GetComponent<Collider>();

        time = GrowTime + GrowDelay + 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kill"))
        {
            transform.localScale = Vector3.zero;
            col.enabled = false;
            time = 0;
        }
    }

    void Update()
    {
        if (time > GrowTime + GrowDelay) return;

        time += Time.deltaTime;

        if (time < GrowDelay) return;

        transform.localScale = Vector3.one * ((time - GrowDelay) / GrowTime);

        if (time > GrowDelay + GrownThreshold)
        {
            col.enabled = true;
        }
    }
}
