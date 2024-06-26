using Cinemachine;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public Vector3 _firstPosition;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnSetCameraTarget()
    {
        var player = FindObjectOfType<GunManager>().transform;
        virtualCamera.Follow = player;
    }

    private void OnReset()
    {
        virtualCamera.Follow = null;
        virtualCamera.LookAt = null;
        virtualCamera.transform.localPosition = _firstPosition;

    }

    private void UnSubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
