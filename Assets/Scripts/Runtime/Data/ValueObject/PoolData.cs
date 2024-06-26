using System;
using UnityEngine;

[Serializable]
public struct PoolData
{
    public PoolType Type;
    public GameObject Prefabs;
    public byte Count;
}