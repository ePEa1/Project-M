using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.CamSystem;

[CreateAssetMenu(fileName = "PC Attack Object", menuName = "CharacterSystem/Attack Object", order = int.MaxValue)]
public class PCAtkObject : ScriptableObject
{
    public float distance;
    public AnimationCurve distanceCurve;
    public float rushSpeed;
    public Collider range;
    public GameObject[] effs;
    public Vector3[] effPos;
    public Vector3[] effDir;
    public AudioSource[] sfx;
    public CustomShaking shakeData;
}
