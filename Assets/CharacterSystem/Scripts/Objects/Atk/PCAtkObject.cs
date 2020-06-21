using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.CamSystem;


[CreateAssetMenu(fileName = "PC Attack Object", menuName = "CharacterSystem/Attack Object", order = 2)]
public class PCAtkObject : ScriptableObject
{
    public float distance;
    public AnimationCurve distanceCurve;
    public float rushSpeed;
    public PCAtksData[] atkData;
    public CustomShaking shakeData;
}

[System.Serializable]
public struct PCAtksData
{
    public float getMp;
    public float getGage;
    public float damage;
    public Vector3 colCenter;
    public Vector3 colSize;
    public GameObject eff;
    public Vector3 effPos;
    public Vector3 effDir;
    public AudioClip sfx;
}