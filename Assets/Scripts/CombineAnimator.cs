using UnityEngine;

public class CombineAnimator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] Transform _cylinder;
    [SerializeField] Transform[] rakes;

    void Update()
    {
        var rot = _rotationSpeed * Time.deltaTime;
        _cylinder.Rotate(Vector3.right * rot);

        foreach (var rake in rakes)
        {
            rake.Rotate(Vector3.right * -rot);
        }
    }
}
