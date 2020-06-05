using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetView : MonoBehaviour
{
    Camera cam;
    bool addOnlyOnce;
    [SerializeField] public float YPos; //자꾸 바닥에 생겨서 중간 값을 체크

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        addOnlyOnce = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyPos = cam.WorldToViewportPoint(gameObject.transform.position);

        bool onScreen = enemyPos.z > 0 && enemyPos.x > 0 && enemyPos.x < 1 && enemyPos.y > 0 && enemyPos.y < 1;

        if(onScreen && addOnlyOnce)
        {
            addOnlyOnce = false;
            AutoTargetManager.nearOrder.Add(this);
        }
    }
}
