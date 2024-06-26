using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CD_Pool", menuName = "DeadBalloons/CD_Pool", order = 1)]
public class CD_Pool : ScriptableObject
{
    public List<PoolData> PoolData;
}