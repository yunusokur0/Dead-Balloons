using UnityEngine.Events;
public class GunSignals : MonoSingleton<GunSignals>
{
    public UnityAction<bool> onPlayConditionChanged = delegate { };
    public UnityAction<bool> onMoveConditionChanged = delegate { };
}