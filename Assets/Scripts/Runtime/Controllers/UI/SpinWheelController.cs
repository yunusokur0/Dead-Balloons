using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpinWheelController : MonoBehaviour
{
    [SerializeField] private GameObject collect;
    [SerializeField] private Button collectButton;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject spinObject;
    [SerializeField] private GameObject spinButton;
    [SerializeField] private GameObject moneys;
    [SerializeField] private TextMeshProUGUI rewardSpinText;

    private float spinSpeed = 1f;
    private float spinDecelerationTime = 2.3f;
    private float minSpinSpeed = 30f;
    private float spinStopDelay = 2.3f;
    private float timer = 0f;
    private short currentRewardSpin;
    private int minRandomSpin = 800;
    private int maxRandomSpin = 1300;

    private bool isSpinning = false;
    private bool canSpin = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "one":
                currentRewardSpin = 50; rewardSpinText.text = currentRewardSpin.ToString();
                break;
            case "two":
                currentRewardSpin = 250; rewardSpinText.text = currentRewardSpin.ToString();
                break;
            case "there":
                currentRewardSpin = 1000; rewardSpinText.text = currentRewardSpin.ToString();
                break;
            case "four":
                currentRewardSpin = 90; rewardSpinText.text = currentRewardSpin.ToString();
                break;
            case "five":
                currentRewardSpin = 700; rewardSpinText.text = currentRewardSpin.ToString();
                break;
            case "six":
                currentRewardSpin = 320; rewardSpinText.text = currentRewardSpin.ToString();
                break;
            case "seven":
                currentRewardSpin = 500; rewardSpinText.text = currentRewardSpin.ToString();
                break;
        }
    }

    public void StartSpin()
    {
        spinSpeed = Random.Range(minRandomSpin, maxRandomSpin);
        isSpinning = true;
        spinButton.SetActive(false);
        moneys.SetActive(true);
        collect.SetActive(true);
        StartCoroutine(SpinRoutine());
    }

    public void RewardCollect()
    {
        CoreGameSignals.Instance.onVibrate?.Invoke(60);
        ScoreSignals.Instance.onAddSpinMoney?.Invoke();
        rewardSpinText.text = 0.ToString();
        StartCoroutine(closeSpinMenu());
    }

    private IEnumerator closeSpinMenu()
    {
        yield return new WaitForSecondsRealtime(spinStopDelay);
        closeButton.SetActive(true);
    }

    public void OnClosePanle()
    {
        CoreUISignals.Instance.onClosePanel?.Invoke(5);
    }

    private IEnumerator SpinRoutine()
    {
        isSpinning = true;
        collectButton.interactable = false;
        spinButton.SetActive(false);
        moneys.SetActive(true);
        collect.SetActive(true);

        while (isSpinning)
        {
            spinObject.transform.Rotate(0, 0, spinSpeed * Time.deltaTime);

            if (timer >= spinDecelerationTime && spinSpeed >= minSpinSpeed)
            {
                DecelerateSpin();
            }
            else if (spinSpeed <= minSpinSpeed)
            {
                StopSpin();
                break; 
            }

            timer += Time.deltaTime;
            yield return null; 
        }
    }

    private void DecelerateSpin()
    {
        spinSpeed = Mathf.Lerp(spinSpeed, 0, Time.deltaTime);
        canSpin = false;
    }

    private void StopSpin()
    {
        spinSpeed = 0;
        collectButton.interactable = true;
        canSpin = true;
    }

    private short OnCurrentRewarSpin() => currentRewardSpin;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        UISignals.Instance.onSpinlMoneyValue += OnCurrentRewarSpin;
    }

    private void UnsubscribeEvents()
    {
        UISignals.Instance.onSpinlMoneyValue -= OnCurrentRewarSpin;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}