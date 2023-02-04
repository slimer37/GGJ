using UnityEngine;

public class ParticleAnimation : MonoBehaviour
{
    [SerializeField] ParticleSystem _enterDirt;

    public void EnterDirt()
    {
        _enterDirt.Play();
    }
}
