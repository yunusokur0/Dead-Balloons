using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        SaveSignals.Instance.onSaveGameData += SaveData;
    }

    private void SaveData()
    {
        OnSaveGame(
            new SaveGameDataParams()
            {
                Money = SaveSignals.Instance.onGetMoney(),
                DamageMoney = SaveSignals.Instance.onDamageMoney(),
                SpawmMoney = SaveSignals.Instance.onSpawmMoney(),
                Level = SaveSignals.Instance.onGetLevelID(),
                Damage = SaveSignals.Instance.onGetDamage(),
                SpawmLevel = SaveSignals.Instance.onSpawmLevel(),
                SpawmSpeed = SaveSignals.Instance.onGetSpawmSpeed(),
                SoundPref = SaveSignals.Instance.SoundPref(),
                VibrationPref = SaveSignals.Instance.VibrationPref(),
            }
        );
    }

    private void OnSaveGame(SaveGameDataParams saveDataParams)
    {
        if (saveDataParams.Level != null) ES3.Save("Level", saveDataParams.Level);
        if (saveDataParams.VibrationPref != null) ES3.Save("VibrationPref", saveDataParams.VibrationPref);
        if (saveDataParams.SoundPref != null) ES3.Save("SoundPref", saveDataParams.SoundPref);
        if (saveDataParams.Damage != null) ES3.Save("Damage", saveDataParams.Damage);
        if (saveDataParams.SpawmLevel != null) ES3.Save("SpawmLevel", saveDataParams.SpawmLevel);
        if (saveDataParams.SpawmSpeed != null) ES3.Save("SpawmSpeed", saveDataParams.SpawmSpeed);
        if (saveDataParams.Money != null) ES3.Save("Money", saveDataParams.Money);
        if (saveDataParams.DamageMoney != null) ES3.Save("DamageMoney", saveDataParams.DamageMoney);
        if (saveDataParams.SpawmMoney != null) ES3.Save("SpawmMoney", saveDataParams.SpawmMoney);
    }

    private void UnsubscribeEvents()
    {
        SaveSignals.Instance.onSaveGameData -= SaveData;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}