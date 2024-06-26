using TMPro;
using UnityEngine;

public class WinPanelController : MonoSingleton<WinPanelController>
{
    [SerializeField] private TextMeshProUGUI bonusMoneyText;
    [SerializeField] private TextMeshProUGUI deadBalloonText;
    [SerializeField] private TextMeshProUGUI XValue;

    private const int BonusMultiplier = 18;

    public void UpdateWinUITextValues()
    {
        deadBalloonText.text = ScoreSignals.Instance.onSetDeadBalloonValue.ToString();
        bonusMoneyText.text = (ScoreSignals.Instance.onSetDeadBalloonValue * 18).ToString();
        XValue.text = ScoreSignals.Instance.onSetMiniGameLevelValue.ToString();
    }

    public void asd()
    {
        bonusMoneyText.text = 0.ToString();
    }
    private void OnEnable() => SubscribeEvents();

    private void SubscribeEvents()
    {
        ScoreSignals.Instance.UpdateWinUITextValues += UpdateWinUITextValues;
    }

    private void UnSubscribeEvents()
    {
        ScoreSignals.Instance.UpdateWinUITextValues -= UpdateWinUITextValues;
    }

    private void OnDisable() => UnSubscribeEvents();
}
