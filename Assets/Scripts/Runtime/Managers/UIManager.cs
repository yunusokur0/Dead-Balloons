using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnLevelInitialize(byte levelValue)
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Money, 0);
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 2);
        if (levelValue >= 3) { CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Shop, 3); }
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 4);
    }

    public void OnPlay()
    {
        CameraSignals.Instance.onSetCameraTarget?.Invoke();
        CoreGameSignals.Instance.onPlay?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(3);
        CoreUISignals.Instance.onClosePanel?.Invoke(4);
    }
    public void OnNextLevel()
    {
        ScoreSignals.Instance.onAddWinMoney?.Invoke();
        ScoreSignals.Instance.onSetDeadBalloonValue = 0;
        StartCoroutine(WaitForFinal());
    }

    public void OnRestartLevel()
    {
        ScoreSignals.Instance.onSetDeadBalloonValue = 0;
        CoreUISignals.Instance.onClosePanel?.Invoke(3);
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.NextLoading, 6);
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
    }

    private void OnLevelFailed()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 3);
    }

    private void OnLevelSuccessful()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 3);
    }

    public void OnIncomeUpdate()
    {
        CoreGameSignals.Instance.onVibrate?.Invoke(45);
        UISignals.Instance.onClickSpawmSpeed?.Invoke();
    }

    public void OnDamageUpdate()
    {
        CoreGameSignals.Instance.onVibrate?.Invoke(45);
        UISignals.Instance.onClickDamage?.Invoke();
    }

    IEnumerator WaitForFinal()
    {
        CoreGameSignals.Instance.onVibrate?.Invoke(70);
        CoreGameSignals.Instance.dolaranim?.Invoke();
        yield return new WaitForSeconds(2.5f);
        CoreUISignals.Instance.onClosePanel?.Invoke(3);
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.NextLoading, 6);
        CoreGameSignals.Instance.onNextLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}