using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStat : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 7.5f;
    [SerializeField] public float rotateSpeed = 540;
    [SerializeField] public float fallSpeed = 20;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackRate;


    [SerializeField] public float hp;
    public float currentHp;
    public MobStat lastHitBy = null;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHp = 100;
        currentHp = hp;
    }

}
