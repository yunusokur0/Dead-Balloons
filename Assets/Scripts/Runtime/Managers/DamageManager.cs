using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public short _damage;
    public short damage;
    private int _newMoney;
    public int _damageMoney;
    private void Awake()
    {
        _damage = LoadDamageData();
        _damageMoney = GetDamageMoneyValue();
    }

    private void Start()
    {
        damage = _damage;
    }
    private int GetDamageMoneyValue()
    {
        if (!ES3.FileExists()) return 50;
        return (int)(ES3.KeyExists("DamageMoney") ? ES3.Load<int>("DamageMoney") : 50);
    }
    private void OnEnable() => Subscription();

    private void OnRefreshDamage()
    {
        damage = _damage;
    }
    private void Subscription()
    {
        BulletSignals.Instance.onRefreshDamage += OnRefreshDamage;
        BulletSignals.Instance.onBulletTriggerXDamage += OnTriggerXDamageWall;
        BulletSignals.Instance.onBulletTriggerIDamage += OnTriggerIDamageWall;
        CoreGameSignals.Instance.onGetDamageLevel += OnGetDamageLevel;
        SaveSignals.Instance.onGetDamage += GetDamageValue;
        UISignals.Instance.onClickDamage += OnClickDamage;
        SaveSignals.Instance.onDamageMoney += OnGetDamageMoneyValue;
    }

    private short LoadDamageData()
    {
        if (!ES3.FileExists()) return 2;
        return (byte)(ES3.KeyExists("Damage") ? ES3.Load<short>("Damage") : 2);
    }
    private void OnTriggerXDamageWall()
    {
        damage += (short)(_damage/2);
    }

    private void OnTriggerIDamageWall()
    {
        damage -= (short)(_damage / 2);
    }

    private void OnClickDamage()
    {
        _newMoney = (int)(SaveSignals.Instance.onGetMoney() - SaveSignals.Instance.onDamageMoney());
        _damage += 1;
        _damageMoney += 100;
        damage = _damage;

        UISignals.Instance.onSetDamageLvlText?.Invoke();
        ScoreSignals.Instance.onReturnMoneyText?.Invoke((int)_newMoney);
        ScoreSignals.Instance.onGetMoneyValue?.Invoke();
        UISignals.Instance.onChangesSpawmIntaractable?.Invoke();
        SaveSignals.Instance.onSaveGameData?.Invoke();
    }
    public int OnGetDamageMoneyValue() { return _damageMoney; }
    public short GetDamageValue() => _damage;
    private byte OnGetDamageLevel() { return (byte)(_damage); }

    private void UnSubscription()
    {
        BulletSignals.Instance.onRefreshDamage -= OnRefreshDamage;
        BulletSignals.Instance.onBulletTriggerXDamage -= OnTriggerXDamageWall;
        BulletSignals.Instance.onBulletTriggerIDamage -= OnTriggerIDamageWall;
        SaveSignals.Instance.onGetDamage -= GetDamageValue;
        UISignals.Instance.onClickDamage -= OnClickDamage;
        CoreGameSignals.Instance.onGetDamageLevel -= OnGetDamageLevel;
        SaveSignals.Instance.onDamageMoney -= OnGetDamageMoneyValue;
    }

    private void OnDisable() => UnSubscription();
}