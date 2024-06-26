using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageLvlText;
    [SerializeField] private TextMeshProUGUI damageMoneyValue;
    [SerializeField] private TextMeshProUGUI spawmLvlText;
    [SerializeField] private TextMeshProUGUI spawnMoneyValue;
    [SerializeField] private Button damageButton;
    [SerializeField] private Button spawmButton;

    private const float DisabledButtonAlpha = 0.3574226f;

    private void Awake()
    {
        UpdateDamageValue();
        UpdateSpawmValue();
    }

    private void Start()
    {
        OnChangesDamageIntaractable();
        OnChangesSpawmIntaractable();
        UpdateLevelTexts();
    }

    private string FormatNumber(int number)
    {
        return NumberFormatter.Instance.FormatNumber(number);
    }

    private void UpdateDamageValue()
    {
        int damage = SaveSignals.Instance.onDamageMoney();
        damageMoneyValue.text = FormatNumber(damage);
    }

    private void UpdateLevelTexts()
    {
        damageLvlText.text = $"LV {CoreGameSignals.Instance.onGetDamageLevel()}";
        spawmLvlText.text = $"LV {CoreGameSignals.Instance.onGetSpawmLevel()}";
    }

    private void UpdateSpawmValue()
    {
        int spawm = SaveSignals.Instance.onSpawmMoney();
        spawnMoneyValue.text = FormatNumber(spawm);
    }

    private void OnSetDamageLvLText()
    {
        damageLvlText.text = "LV " + CoreGameSignals.Instance.onGetDamageLevel().ToString();
        int damage = SaveSignals.Instance.onDamageMoney();
        damageMoneyValue.text = FormatNumber(damage);
        SaveSignals.Instance.onSaveGameData?.Invoke();
    }

    private void OnSetSpawmLvLText()
    {
        spawmLvlText.text = "LV " + CoreGameSignals.Instance.onGetSpawmLevel().ToString();
        int spawm = SaveSignals.Instance.onSpawmMoney();
        spawnMoneyValue.text = FormatNumber(spawm);
        SaveSignals.Instance.onSaveGameData?.Invoke();
    }

    private void OnChangesSpawmIntaractable()
    {
        if ((ScoreSignals.Instance.onGetMoneyValue()) < SaveSignals.Instance.onSpawmMoney())
        {
            spawmButton.interactable = false;
            spawmButton.image.color = new Color(DisabledButtonAlpha, DisabledButtonAlpha, DisabledButtonAlpha);
        }

        else
        {
            spawmButton.interactable = true;
        }
    }

    private void OnChangesDamageIntaractable()
    {
        if ((ScoreSignals.Instance.onGetMoneyValue()) < SaveSignals.Instance.onDamageMoney())
        {
            damageButton.interactable = false;
            damageButton.image.color = new Color(DisabledButtonAlpha, DisabledButtonAlpha, DisabledButtonAlpha);
        }
        else
        {
            damageButton.interactable = true;
        }
    }
    private void qwer()
    {
        OnChangesSpawmIntaractable();
        OnChangesDamageIntaractable();
    }
    private void SpawmButton() => qwer();

    private void OnEnable() => SubscribeEvents();
    private void SubscribeEvents()
    {
        UISignals.Instance.onSetSpawmLvlText += OnSetSpawmLvLText;
        UISignals.Instance.onSetDamageLvlText += OnSetDamageLvLText;
        UISignals.Instance.onChangesSpawmIntaractable += SpawmButton;
    }

    private void UnSubscribeEvents()
    {
        UISignals.Instance.onSetSpawmLvlText -= OnSetSpawmLvLText;
        UISignals.Instance.onSetDamageLvlText -= OnSetDamageLvLText;
        UISignals.Instance.onChangesSpawmIntaractable -= SpawmButton;
    }
    private void OnDisable() => UnSubscribeEvents();
}