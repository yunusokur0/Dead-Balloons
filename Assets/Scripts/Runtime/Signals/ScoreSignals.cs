using System;
using UnityEngine.Events;

public class ScoreSignals : MonoSingleton<ScoreSignals>
{
    public string onSetMiniGameLevelValue;
    public byte onSetDeadBalloonValue;

    public UnityAction<int> onReturnMoneyText = delegate { };
    public UnityAction onAddMoneyTrigger = delegate { };
    public UnityAction onAddWinMoney = delegate { };
    public Func<int> onGetMoneyValue = delegate { return 0; };
    public UnityAction onAddSpinMoney = delegate { };
    public UnityAction UpdateWinUITextValues = delegate { };
    //public Func<short, short> onSetDeadBalloonValue = delegate { return default; };
}