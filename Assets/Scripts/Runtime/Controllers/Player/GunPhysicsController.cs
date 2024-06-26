using DG.Tweening;
using TMPro;
using UnityEngine;

public class GunPhysicsController : MonoBehaviour
{
    [SerializeField] private Rigidbody managerRigidbody;
    private float _changesColor;
    [SerializeField] private GunMovementController movementController;
    public static int ses;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            other.transform.DOScale(0, 1f).SetEase(Ease.OutExpo);
            CoreGameSignals.Instance.onVibrate?.Invoke(50);
            ScoreSignals.Instance.onAddMoneyTrigger?.Invoke();
            PoolSignals.Instance.onReturnToPool?.Invoke(other.gameObject, PoolType.Money);
        }

        else if (other.CompareTag("MiniGameBalloon"))
        {
            CoreGameSignals.Instance.onVibrate?.Invoke(55);
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            ScoreSignals.Instance.UpdateWinUITextValues?.Invoke();
        }

        else if (other.CompareTag("Balloon"))
        {
            Destroy(other.gameObject);
            CoreGameSignals.Instance.onVibrate?.Invoke(45);
            CoreGameSignals.Instance.onLevelFailed?.Invoke();
        }

        else if (other.CompareTag("2xdamage"))
        {
            CoreGameSignals.Instance.onVibrate?.Invoke(35);
            BulletSignals.Instance.onBulletTriggerXDamage?.Invoke();
            other.transform.DOScale(0, 0.05f).SetEase(Ease.OutBack);
        }

        else if (other.CompareTag("2Idamage"))
        {
            CoreGameSignals.Instance.onVibrate?.Invoke(35);
            BulletSignals.Instance.onBulletTriggerIDamage?.Invoke();
            other.transform.DOScale(0, 0.05f).SetEase(Ease.OutBack);
        }

        else if (other.CompareTag("2xbullet"))
        {
            CoreGameSignals.Instance.onVibrate?.Invoke(35);
            BulletSignals.Instance.onBulletTriggerSpawn?.Invoke(-0.025f);
            other.transform.DOScale(0, 0.1f).SetEase(Ease.OutBack);
        }

        else if (other.CompareTag("2Ibullet"))
        {
            CoreGameSignals.Instance.onVibrate?.Invoke(35);
            BulletSignals.Instance.onBulletTriggerSpawn?.Invoke(0.025f);
            other.transform.DOScale(0, 0.1f).SetEase(Ease.OutBack);
        }

        else if (other.CompareTag("Wall"))
        {
            ScoreSignals.Instance.onSetMiniGameLevelValue = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;
        }

        else if (other.CompareTag("Obstacle"))
        {
            CoreGameSignals.Instance.onVibrate?.Invoke(60);
            CoreGameSignals.Instance.onLevelFailed?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!movementController._isReadyToPlay)
        {
            if (other.CompareTag("Wall"))
            {
                _changesColor = (0.0036f + _changesColor) % 1;
                other.gameObject.GetComponent<Renderer>().material.DOColor(Color.HSVToRGB(_changesColor, 1, 1), 0.25f);
            }
        }
    }
}