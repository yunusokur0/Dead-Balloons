using TMPro;
using UnityEngine;

public class LevelPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private void OnSetLevelText(int value)
    {
        levelText.text = "Level " + (value + 1);
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        UISignals.Instance.onSetLevelText += OnSetLevelText;
    }

    private void UnSubscribeEvents()
    {
        UISignals.Instance.onSetLevelText -= OnSetLevelText;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}