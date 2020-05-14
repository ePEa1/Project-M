using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AliceDAMAGE : AliceFSMState
{
    public GameObject hiteff;
    public Slider HPGauge;
     AtkCollider damInfo;
    public EnemyHPViewManager HpManager;

    public bool IsDamaged = false;
    public override void BeginState()
    {
        base.BeginState();
    }


    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<AliceFSMManager>();
        HPGauge = GameObject.FindGameObjectWithTag("HPGauge").GetComponent<Slider>();
        HpManager = HPGauge.GetComponent<EnemyHPViewManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(IsDamaged == true)
        //{
            


        //}

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PCAtkCollider")
        {
            damInfo = other.GetComponent<AtkCollider>();
            manager.anim.SetInteger("curState", 3);
            IsDamageCheck();
            HpManager.ShowHP();
            KnockBack();
        }
    }
    void IsDamageCheck()
    {
        Debug.Log("Isdamage");
        CreatHitEff();
        IsDamaged = true;
        manager.CurAliceHP -= 1;//후에 데미지로 변경
        Debug.Log(manager.CurAliceHP);
        HpManager.GaugeVal = manager.CurAliceHP;
    }
    void CreatHitEff()
    {
        GameObject curhiteff = Instantiate(hiteff);
        curhiteff.transform.position = transform.position;
    }

    void enemyknockback()
    {
        manager.transform.position = manager.transform.position + new Vector3(0, 0, 5);
    }

    void KnockBack()
    {
        Debug.Log("startcorutine");
        Vector3 knockbackPos = manager.transform.position + damInfo.knockVec * damInfo.knockPower;
        manager.transform.position = knockbackPos;

        //yield return new WaitForSeconds(0.3f);
        IsDamaged = false;
    }

}
