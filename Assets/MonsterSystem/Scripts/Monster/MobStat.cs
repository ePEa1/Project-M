using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStat : MonoBehaviour
{

    public float moveSpeed = 7.5f;
    public float rotateSpeed = 540;
    public float fallSpeed = 20;
    public float attackRange;
    public float attackRate;


    public float hp;
    public float currentHp;
    public MobStat lastHitBy = null;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHp = 100;
        currentHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
