using System;
using UnityEngine;
using UnityEngine.Events;

public class PoolSignals : MonoSingleton<PoolSignals>
{
    public Func<PoolType, GameObject> onGetPoolObject = delegate { return default; };
    public UnityAction<GameObject, PoolType> onReturnToPool = delegate { };
}