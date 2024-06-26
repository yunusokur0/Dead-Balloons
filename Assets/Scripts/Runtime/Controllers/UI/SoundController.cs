using UnityEngine;
using UnityEngine.UI;


public class SoundController : MonoBehaviour
{
    [SerializeField] private Button soundMuteButton;
    [SerializeField] private GameObject soundMute;
    private int isSound;

    private void Awake()
    {
        isSound = GetSoundPref();
    }

    void Start()
    {
        soundMuteButton.onClick.AddListener(ToggleSound);      
        UpdateSoundState();
    }

    private int GetSoundPref()
    {
        if (!ES3.FileExists()) return 1;
        return (int)(ES3.KeyExists("SoundPref") ? ES3.Load<int>("SoundPref") : 1);
    }

    private void ToggleSound()
    {
        isSound = (byte)(isSound == 1 ? 0 : 1);
        UpdateSoundState();
        SaveSignals.Instance.onSaveGameData?.Invoke();
    }

    private void UpdateSoundState()
    {
        soundMute.SetActive(isSound == 0);
    }

    private int OnSoundPref() => isSound;

    private void OnEnable() => Subscription();
    private void Subscription()
    {
        SaveSignals.Instance.SoundPref += OnSoundPref;
    }

    private void UnSubscription()
    {
        SaveSignals.Instance.SoundPref -= OnSoundPref;
    }
    private void OnDisable() => UnSubscription();
}