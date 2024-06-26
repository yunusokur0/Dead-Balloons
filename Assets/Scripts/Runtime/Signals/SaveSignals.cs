using System;
using UnityEngine.Events;

public class SaveSignals : MonoSingleton<SaveSignals>
{
    public UnityAction onSaveGameData = delegate { };
    public Func<byte> onGetLevelID = delegate { return 0; };
    public Func<int> onGetMoney = delegate { return 0; };
    public Func<int> onDamageMoney = delegate { return 50; };
    public Func<int> onSpawmMoney = delegate { return 50; };
    public Func<short> onGetDamage = delegate { return 0; };
    public Func<byte> onSpawmLevel = delegate { return 1; };
    public Func<float> onGetSpawmSpeed = delegate { return .55f; };

    public Func<int> SoundPref = delegate { return 1; };
    public Func<int> VibrationPref = delegate { return 1; };
}