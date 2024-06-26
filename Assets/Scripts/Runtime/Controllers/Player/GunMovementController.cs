using UnityEngine;

public class GunMovementController : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;

    private GunData _data;
    private bool _isReadyToMove;
    public bool _isReadyToPlay;
    private float _inputValue;
    private Vector2 _clampValues;

    internal void SetMovementData(GunData movementData)
    {
        _data = movementData;
    }

    public void UpdateInputValue(HorizontalInputParams inputParams)
    {
        _inputValue = inputParams.HorizontalValue;
        _clampValues = inputParams.ClampValues;
    }

    private void FixedUpdate()
    {
        if (_isReadyToPlay)
        {
            if (_isReadyToMove)
            {
                Move();
            }
            else
            {
                StopSideways();
            }
        }
        else
            Stop();
    }

    private void Move()
    {
        var newVelocity = new Vector3(_inputValue * _data.SidewaysSpeed, rigidbody.velocity.y, _data.ForwardSpeed);
        rigidbody.velocity = newVelocity;

        var newPosition = rigidbody.position;
        newPosition.x = Mathf.Clamp(newPosition.x, _clampValues.x, _clampValues.y);
        rigidbody.position = newPosition;
    }

    private void StopSideways()
    {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _data.ForwardSpeed);
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void Stop()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    public void OnReset()
    {
        Stop();
        _isReadyToPlay = false;
        _isReadyToMove = false;
    }

    private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;
    private void OnMoveConditionChanged(bool condition) => _isReadyToMove = condition;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GunSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
        GunSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnSubscribeEvents()
    {
        GunSignals.Instance.onPlayConditionChanged -= OnPlayConditionChanged;
        GunSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
