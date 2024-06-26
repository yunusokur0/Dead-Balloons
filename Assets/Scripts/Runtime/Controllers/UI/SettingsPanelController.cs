using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    [SerializeField] private Button settingButton;
    [SerializeField] private Animator backgroundAnimator;
    [SerializeField] private Animator layoutBackgroundAnimator;

    private bool isSettingsOpen = false;

    private void Start()
    {
        settingButton.onClick.AddListener(ToggleSettings);
    }

    private void ToggleSettings()
    {
        string trigger = isSettingsOpen ? "close" : "open";
        backgroundAnimator.SetTrigger(trigger);
        layoutBackgroundAnimator.SetTrigger(trigger);
        isSettingsOpen = !isSettingsOpen;
    }
}
