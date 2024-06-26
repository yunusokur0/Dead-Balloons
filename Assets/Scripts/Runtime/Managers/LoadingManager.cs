using UnityEngine;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private bool tf;
    [SerializeField] private float time;

    private void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        if(tf)
        {
            yield return new WaitForSeconds(time);
            CoreUISignals.Instance.onClosePanel?.Invoke(7);
        }
        else
        {
            yield return new WaitForSeconds(time);
            CoreUISignals.Instance.onClosePanel?.Invoke(6);
        }
    }
}