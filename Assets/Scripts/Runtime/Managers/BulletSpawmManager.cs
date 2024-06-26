using UnityEngine;


public class BulletSpawmManager : MonoBehaviour
{
    public float _spawmRate;
    public float spawmRate;
    private float _spawnRateIncrease = -0.005f;
    private int _newMoney;
    private byte _spawmLevel = 1;
    private int _spawmMoney;
    private void Awake()
    {
        _spawmRate = LoadSpawmData();
        _spawmLevel = LoadSpawmLevelData();
        _spawmMoney = GetSpawmMoneyValue();
    }

    private void Start()
    {
        spawmRate = _spawmRate;
    }

    private void OnBulletTriggerSpawn(float value)
    {
        spawmRate += value;
    }
    private void OnRefreshDamage()
    {
        spawmRate = _spawmRate;
    }

    private void OnClickSpawmSpeed()
    {
        _newMoney = (int)(ScoreSignals.Instance.onGetMoneyValue() - SaveSignals.Instance.onSpawmMoney());
        _spawmRate += _spawnRateIncrease;
        _spawmLevel += 1;
        _spawmMoney += 175;
        spawmRate = _spawmRate;
        ScoreSignals.Instance.onReturnMoneyText?.Invoke((int)_newMoney);
        UISignals.Instance.onSetSpawmLvlText?.Invoke();
        UISignals.Instance.onChangesSpawmIntaractable?.Invoke();
        SaveSignals.Instance.onSaveGameData?.Invoke();
    }

    private int GetSpawmMoneyValue()
    {
        if (!ES3.FileExists()) return 50;
        return (int)(ES3.KeyExists("SpawmMoney") ? ES3.Load<int>("SpawmMoney") : 50);
    }
    private byte LoadSpawmLevelData()
    {
        if (!ES3.FileExists()) return 1;
        return (byte)(ES3.KeyExists("SpawmLevel") ? ES3.Load<byte>("SpawmLevel") : 1);
    }
    private float LoadSpawmData()
    {
        if (!ES3.FileExists()) return .65f;
        return (float)(ES3.KeyExists("SpawmSpeed") ? ES3.Load<float>("SpawmSpeed") : .65f);
    }

    public float OnGetSpawmSpeed() => _spawmRate;
    private byte OnGetSpawmLevel() => _spawmLevel;
    private int OnGetSpawmMoneyValue() { return _spawmMoney; }
    private void OnEnable() => Subscription();

    private void Subscription()
    {
        BulletSignals.Instance.onRefreshBulletSpawn += OnRefreshDamage;
        BulletSignals.Instance.onBulletTriggerSpawn += OnBulletTriggerSpawn;
        SaveSignals.Instance.onGetSpawmSpeed += OnGetSpawmSpeed;
        SaveSignals.Instance.onSpawmLevel += OnGetSpawmLevel;
        UISignals.Instance.onClickSpawmSpeed += OnClickSpawmSpeed;
        CoreGameSignals.Instance.onGetSpawmLevel += OnGetSpawmLevel;
        SaveSignals.Instance.onSpawmMoney += OnGetSpawmMoneyValue;
    }

    private void UnSubscription()
    {
        BulletSignals.Instance.onRefreshBulletSpawn -= OnRefreshDamage;
        BulletSignals.Instance.onBulletTriggerSpawn -= OnBulletTriggerSpawn;
        SaveSignals.Instance.onGetSpawmSpeed -= OnGetSpawmSpeed;
        SaveSignals.Instance.onSpawmLevel -= OnGetSpawmLevel;
        UISignals.Instance.onClickSpawmSpeed -= OnClickSpawmSpeed;
        CoreGameSignals.Instance.onGetSpawmLevel += OnGetSpawmLevel;
        SaveSignals.Instance.onSpawmMoney -= OnGetSpawmMoneyValue;
    }

    private void OnDisable() => UnSubscription();
}
