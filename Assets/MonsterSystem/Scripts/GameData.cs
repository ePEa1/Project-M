using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GameData : MonoBehaviour
{
    public int BackgroundSound = 100;
    public int EffectSound = 100;
    public int MouseMoving = 100;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
