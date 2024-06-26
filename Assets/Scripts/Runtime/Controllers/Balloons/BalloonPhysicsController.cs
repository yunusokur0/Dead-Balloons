using UnityEngine;

public class BalloonPhysicsController : MonoBehaviour
{
    [SerializeField] private BalloonsController balloonController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            balloonController.ScaleAndReduceNumber(false);
            PoolSignals.Instance.onReturnToPool(other.gameObject, PoolType.Bullet);
        }

        else if (other.CompareTag("Needle"))
        {
            balloonController.ScaleAndReduceNumber(true);
        }
    }
}