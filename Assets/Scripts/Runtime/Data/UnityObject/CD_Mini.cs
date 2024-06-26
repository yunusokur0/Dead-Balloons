
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "CD_Mini", menuName = "DeadBalloons/CD_Mini", order = 1)]
    public class CD_Mini : ScriptableObject
    {
        public List<MiniGameData> Levels = new List<MiniGameData>();
    }
