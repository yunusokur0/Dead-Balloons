using TMPro;
using UnityEngine;

public class MoneyPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private int _moneyValue;
    private const int TriggerMoneyValue = 70;

    private void Awake()
    {
        _moneyValue = GetMoneyValue();

        OnReturnMoneyText(_moneyValue);
        SaveGameData();
    }

    private int GetMoneyValue()
    {
        if (!ES3.FileExists()) return 0;
        return (int)(ES3.KeyExists("Money") ? ES3.Load<int>("Money") : 0);
    }

    private int OnGetMoneyValue()
    {
        return _moneyValue;
    }

    private void SaveGameData()
    {
        SaveSignals.Instance.onSaveGameData?.Invoke();
    }

    private void FormatNumber(int value)
    {
        string formattedNumber = NumberFormatter.Instance.FormatNumber(value);
        moneyText.text = formattedNumber;
    }

    private void OnReturnMoneyText(int value)
    {
        _moneyValue = value;
        FormatNumber(value);
    }

    private void OnAddMoneyTrigger()
    {
        _moneyValue += TriggerMoneyValue;
        OnReturnMoneyText(_moneyValue);
        SaveGameData();
    }

    private void OnAddWinMoney()
    {
        _moneyValue += (ScoreSignals.Instance.onSetDeadBalloonValue * 18);
        OnReturnMoneyText(_moneyValue);
    }

    private void OnAddSpinMoney()
    {
        _moneyValue += UISignals.Instance.onSpinlMoneyValue();
        OnReturnMoneyText(_moneyValue);
        SaveGameData();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        ScoreSignals.Instance.onGetMoneyValue += OnGetMoneyValue;
        ScoreSignals.Instance.onReturnMoneyText += OnReturnMoneyText;
        ScoreSignals.Instance.onAddMoneyTrigger += OnAddMoneyTrigger;
        ScoreSignals.Instance.onAddWinMoney += OnAddWinMoney;
        ScoreSignals.Instance.onAddSpinMoney += OnAddSpinMoney;
        SaveSignals.Instance.onGetMoney += OnGetMoneyValue;
    }

    private void UnSubscribeEvents()
    {
        ScoreSignals.Instance.onGetMoneyValue -= OnGetMoneyValue;
        ScoreSignals.Instance.onReturnMoneyText -= OnReturnMoneyText;
        ScoreSignals.Instance.onAddMoneyTrigger -= OnAddMoneyTrigger;
        ScoreSignals.Instance.onAddWinMoney -= OnAddWinMoney;
        ScoreSignals.Instance.onAddSpinMoney -= OnAddSpinMoney;
        SaveSignals.Instance.onGetMoney -= OnGetMoneyValue;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}