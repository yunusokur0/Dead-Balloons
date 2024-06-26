using UnityEngine.Events;

public class CoreUISignals : MonoSingleton<CoreUISignals>
{
    public UnityAction<UIPanelTypes, byte> onOpenPanel = delegate { };
    public UnityAction<byte> onClosePanel = delegate { };
}