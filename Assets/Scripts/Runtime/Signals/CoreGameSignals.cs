using System;
using UnityEngine.Events;

public class CoreGameSignals : MonoSingleton<CoreGameSignals>
{
    public UnityAction<byte> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction dolaranim = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public UnityAction<byte> onVibrate = delegate { };

    public Func<byte> onGetDamageLevel = delegate { return 50; };
    public Func<byte> onGetSpawmLevel = delegate { return 50; };
    public UnityAction<byte> onNewScene = delegate { };
}
