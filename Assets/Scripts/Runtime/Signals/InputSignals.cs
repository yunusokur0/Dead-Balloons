using UnityEngine.Events;

public class InputSignals : MonoSingleton<InputSignals>
{
    public UnityAction onInputTaken = delegate { };
    public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
    public UnityAction onInputReleased = delegate { };
}