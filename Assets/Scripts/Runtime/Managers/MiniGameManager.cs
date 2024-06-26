using TMPro;
using UnityEngine;


public class MiniGameManager : MonoBehaviour
{
    //private const string PlayerDataPath = "Data/CD_Mini";
    //private TextMeshPro _balloonText;
    //private MiniGamePoolData _data;
    //public int stageID;
    //private DamageManager _damageManager;
    //public short damage;

    //private void Awake()
    //{
    //    _damageManager = FindObjectOfType<DamageManager>();
    //}

    //private void Start()
    //{
    //    _balloonText = transform.GetChild(0).GetComponent<TextMeshPro>();
    //    _data = GetPoolData();
    //    SetRequiredAmountText();
    //    damage = _damageManager.GetDamageValue();
    //}

    //private void OnEnable() => Subscription();

    //private void Subscription()
    //{
    //    UISignals.Instance.onClickDamage += OnClickDamage;
    //}

    //private MiniGamePoolData GetPoolData()
    //{
    //    return Resources.Load<CD_Mini>(PlayerDataPath)
    //        .Levels[(int)SaveSignals.Instance.onGetLevelID?.Invoke()].MiniGameBalloonsS[stageID];
    //}

    //private void SetRequiredAmountText()
    //{
    //    int dataBalloonText = _data.BalloonsTextValue;
    //    string formattedNumber = NumberFormatter.Instance.FormatNumber(dataBalloonText);
    //    _balloonText.text = formattedNumber;
    //}
    //private void OnClickDamage()
    //{
    //    damage = _damageManager.GetDamageValue();
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Bullet"))
    //    {
    //        int dataBalloonDamageText = _data.BalloonsTextValue;
    //        int damageValue = dataBalloonDamageText;

    //        damageValue -= damage;
    //        _balloonText.text = damageValue.ToString();
    //        _data.BalloonsTextValue -= damage;

    //        int dataBalloonText = _data.BalloonsTextValue;
    //        string formattedNumber = NumberFormatter.Instance.FormatNumber(dataBalloonText);
    //        _balloonText.text = formattedNumber;

    //        if (_data.BalloonsTextValue <= 0)
    //        {
    //            ScoreSignals.Instance.onSetDeadBalloonValue++;
    //            Destroy(gameObject);
    //        }
    //        other.gameObject.SetActive(false);
    //    }
    //}
    //public int OnDeadBalloons() { return ScoreSignals.Instance.onSetDeadBalloonValue; }
    //private void UnSubscription()
    //{
    //    UISignals.Instance.onClickDamage -= OnClickDamage;
    //}

    //private void OnDisable() => UnSubscription();
}