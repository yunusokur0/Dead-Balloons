using System.Collections;
using UnityEngine;


public class ParticleController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DeactivateParticle(gameObject));
    }

    private IEnumerator DeactivateParticle(GameObject particle)
    {
        yield return new WaitForSeconds(0.8f);
        PoolSignals.Instance.onReturnToPool?.Invoke(particle, PoolType.Particle);
    }
}