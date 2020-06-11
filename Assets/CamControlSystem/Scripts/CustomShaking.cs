using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.CamSystem
{
    [System.Serializable]
    public struct ShakingKey
    {
        public float skeyTime;
        public Vector3 sKeyPos;
        public Vector3 sKeyDir;
        public AnimationCurve sKeyCurve;
    }

    [CreateAssetMenu(fileName = "Custom Shaking", menuName = "CamSystem/Custom Shaking Data", order = int.MaxValue)]
    public class CustomShaking : ScriptableObject
    {
        [SerializeField]
        ShakingKey[] shakingKeys;
        public ShakingKey[] ShakingData { get { return shakingKeys; } }
    }
}