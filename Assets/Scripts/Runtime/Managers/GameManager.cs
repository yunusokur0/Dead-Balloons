using UnityEngine;

public class GameManager : MonoBehaviour
{
    private byte targetFrameRate = 60;

    private void Start()
    {
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = targetFrameRate;
    }

    private void Update()
    {
        if (Application.targetFrameRate != targetFrameRate)
            Application.targetFrameRate = targetFrameRate;
    }
}