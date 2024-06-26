using UnityEngine;
using UnityEngine.UI;


public class VibrationController : MonoBehaviour
{
    [SerializeField] private Button vibrationButton;
    [SerializeField] private GameObject vibrationMute;
    private int isVibration;

    private void Awake()
    {
        isVibration = GetSoundPref();
    }
    void Start()
    {
        vibrationButton.onClick.AddListener(ToggleVibration);
        UpdateSoundState();
    }

    private int GetSoundPref()
    {
        if (!ES3.FileExists()) return 1;
        return (int)(ES3.KeyExists("VibrationPref") ? ES3.Load<int>("VibrationPref") : 1);
    }

    public void ToggleVibration()
    {
        isVibration = (byte)(isVibration == 1 ? 0 : 1);
        UpdateSoundState();
        SaveSignals.Instance.onSaveGameData?.Invoke();
    }

    private void UpdateSoundState()
    {
        vibrationMute.SetActive(isVibration == 0);
    }

    private void OnVibrate(byte value)
    {
        if (isVibration==1)
            VibrationManager.Vibrate(value);
    }

    public int OnVibrationPref() => isVibration;
    //{
    //    return isVibration;
    //}

    private void OnEnable() => Subscription();
    private void Subscription()
    {
        SaveSignals.Instance.VibrationPref += OnVibrationPref;
        CoreGameSignals.Instance.onVibrate += OnVibrate;
    }

    private void UnSubscription()
    {
        SaveSignals.Instance.VibrationPref -= OnVibrationPref;
        CoreGameSignals.Instance.onVibrate -= OnVibrate;
    }
    private void OnDisable() => UnSubscription();
}