using System;
using UnityEngine.Events;


public class UISignals : MonoSingleton<UISignals>
{
    public UnityAction onSetSpawmLvlText = delegate { };
    public UnityAction onSetDamageLvlText = delegate { };
    public UnityAction<int> onSetLevelText;
    public UnityAction onClickSpawmSpeed = delegate { };
    public UnityAction onClickDamage = delegate { };
    public UnityAction onChangesSpawmIntaractable = delegate { };
    public Func<short> onSpinlMoneyValue = delegate { return 0; };
}
