using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.PlayerData
{
    [CreateAssetMenu(fileName = "AtkPowData", menuName = "CharacterSystem/Attack Power Data", order = 2)]
    public class AtkPowerData : ScriptableObject
    {
        public PowerLevel[] level;
    }

    [System.Serializable]
    public class PowerLevel
    {
        public float power;
        public float nextGage;
        public float minusSpeed;
    }
}