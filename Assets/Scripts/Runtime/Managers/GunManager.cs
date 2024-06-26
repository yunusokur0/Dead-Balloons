using UnityEngine;


public class GunManager : MonoBehaviour
{
    [SerializeField] private GunMovementController movementController;
    private GunData _data;
    private const string PlayerDataPath = "Data/CD_Gun";

    private void Awake()
    {
        _data = GetPlayerData();
        SendPlayerDataToControllers();
    }

    private GunData GetPlayerData() => Resources.Load<CD_Gun>(PlayerDataPath).Data;

    private void SendPlayerDataToControllers()
    {
        movementController.SetMovementData(_data);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputTaken += () => GunSignals.Instance.onMoveConditionChanged?.Invoke(true);
        InputSignals.Instance.onInputReleased += () => GunSignals.Instance.onMoveConditionChanged?.Invoke(false);
        InputSignals.Instance.onInputDragged += OnInputDragged;
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onLevelSuccessful +=
            () => GunSignals.Instance.onPlayConditionChanged?.Invoke(false);
        CoreGameSignals.Instance.onLevelFailed +=
            () => GunSignals.Instance.onPlayConditionChanged?.Invoke(false);
        CoreGameSignals.Instance.onReset += OnReset;
    }


    private void OnPlay()
    {
        GunSignals.Instance.onPlayConditionChanged?.Invoke(true);
    }

    private void OnInputDragged(HorizontalInputParams inputParams)
    {
        movementController.UpdateInputValue(inputParams);
    }
    private void OnReset()
    {
        movementController.OnReset();
    }

    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputTaken -= () => GunSignals.Instance.onMoveConditionChanged?.Invoke(true);
        InputSignals.Instance.onInputReleased -= () => GunSignals.Instance.onMoveConditionChanged?.Invoke(false);
        InputSignals.Instance.onInputDragged -= OnInputDragged;
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onLevelSuccessful -=
            () => GunSignals.Instance.onPlayConditionChanged?.Invoke(false);
        CoreGameSignals.Instance.onLevelFailed -=
            () => GunSignals.Instance.onPlayConditionChanged?.Invoke(false);
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}