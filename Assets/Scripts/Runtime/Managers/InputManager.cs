using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private InputData _data;
    private bool isReadyForTouch, isFirstTimeTouchTaken, _isTouching;
    private float _currentVelocity;
    private Vector2? _mousePosition;
    private Vector3 _moveVector;

    private void Awake()
    {
        _data = GetInputData();
    }

    private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").Data;

    private void OnPlay()
    {
        isReadyForTouch = true;
    }

    private void OnReset()
    {
        _isTouching = false;
        isReadyForTouch = false;
        isFirstTimeTouchTaken = false;
    }


    private void Update()
    {
        if (!isReadyForTouch) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !IsPointerOverUIElement())
            {
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
                if (!isFirstTimeTouchTaken)
                {
                    isFirstTimeTouchTaken = true;
                }
                _mousePosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved && !IsPointerOverUIElement())
            {
                if (_isTouching)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 touchDeltaPos = touch.position - _mousePosition.Value;
                        float scaledInputSpeed = _data.HorizontalInputSpeed / 10f;

                        _moveVector.x = Mathf.Abs(touchDeltaPos.x) > _data.HorizontalInputSpeed
                            ? Mathf.Sign(touchDeltaPos.x) * scaledInputSpeed * Mathf.Abs(touchDeltaPos.x)
                            : Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity, _data.ClampSpeed/10);

                        _mousePosition = touch.position;

                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams
                        {
                            HorizontalValue = _moveVector.x,
                            ClampValues = _data.ClampSides
                        });
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
            }
        }
    }

    //private void Update()
    //{
    //    if (!isReadyForTouch) return;

    //    if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
    //    {
    //        _isTouching = true;
    //        InputSignals.Instance.onInputTaken?.Invoke();
    //        if (!isFirstTimeTouchTaken)
    //        {
    //            isFirstTimeTouchTaken = true;
    //        }
    //        _mousePosition = Input.mousePosition;
    //    }

    //    if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
    //    {
    //        if (_isTouching)
    //        {
    //            if (_mousePosition != null)  
    //            {
    //                Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
    //                float scaledInputSpeed = _data.HorizontalInputSpeed / 10f;

    //                _moveVector.x = Mathf.Abs(mouseDeltaPos.x) > _data.HorizontalInputSpeed
    //                    ? Mathf.Sign(mouseDeltaPos.x) * scaledInputSpeed * Mathf.Abs(mouseDeltaPos.x)
    //                    : Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity, _data.ClampSpeed);

    //                _mousePosition = Input.mousePosition;

    //                InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams
    //                {
    //                    HorizontalValue = _moveVector.x,
    //                    ClampValues = _data.ClampSides  
    //                });

    //            }
    //        }
    //    }

    //    if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
    //    {
    //        _isTouching = false;
    //        InputSignals.Instance.onInputReleased?.Invoke();
    //    }
    //}

    private bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}