using System;
using UnityEngine;

    [Serializable]
    public class InputData
    {
        //yatay Giris Hizi, yatay olarak objenin gitme hizi
        public float HorizontalInputSpeed;
        //border
        public Vector2 ClampSides;
        //nesnenin hızını ne kadar hızlı yavaşlatması gerektiğini kontrol eder.
        public float ClampSpeed;
    }
