using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class BalloonsController : MonoBehaviour
{
    [SerializeField] private TextMeshPro balloonText;
    [SerializeField] private BalloonEnum balloonEnum;
    [SerializeField] private float scale;

    public int current;
    public short damage;

    private DamageManager _damageManager;
    private void Start()
    {
        _damageManager = FindObjectOfType<DamageManager>();
        SetRequiredAmountText();
    }

    private void SetRequiredAmountText()
    {
        int currentNumber = int.Parse(balloonText.text);
        current = currentNumber;
        string formattedNumber = NumberFormatter.Instance.FormatNumber(currentNumber);
        balloonText.text = formattedNumber;
    }

    public void ScaleAndReduceNumber(bool bomb)
    {
        damage = _damageManager.damage;
        current -= damage;

        string formattedNumbe1r = NumberFormatter.Instance.FormatNumber(current);
        balloonText.text = current.ToString();
        balloonText.text = formattedNumbe1r;

        if (current <= 0 || bomb)
        {
            if (balloonEnum == BalloonEnum.Open)
                IsDead();

            Particle();
            gameObject.SetActive(false);
            ScoreSignals.Instance.onSetDeadBalloonValue++;
        }

        Vector3 newScale = transform.localScale + new Vector3(scale, scale, scale);
        transform.DOScale(newScale, 0.3f);
    }

    private void IsDead()
    {
        Money();
   
    }

    private void Money()
    {
        GameObject money = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Money);
        money.transform.SetParent(gameObject.transform.parent.parent);
        money.SetActive(true);
        Vector3 pos = transform.position;
        money.transform.position = pos;
        money.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 10), ForceMode.Impulse);
    }

    private void Particle()
    {
        var particle = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Particle);
        particle.transform.SetParent(gameObject.transform.parent.parent);
        particle.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
        particle.SetActive(true);
    }
}