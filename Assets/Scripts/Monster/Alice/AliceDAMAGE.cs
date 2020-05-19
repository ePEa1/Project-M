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
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PCAtkCollider")
        {
            if(manager.PlayerIsAttack == false)
            {
                manager.PlayerIsAttack = true;
            }
            manager.DamageSound.Play();
            damInfo = other.GetComponent<AtkCollider>();
            //manager.anim.Rebind();
            //manager.anim.Play("DAMAGE");
            IsDamageCheck();
            HpManager.ShowHP();
            other.GetComponent<AtkCollider>().AtkEvent();
            KnockBack();
        }
    }
    void IsDamageCheck()
    {
        CreatHitEff();
        IsDamaged = true;
        manager.CurAliceHP -= 0.5f;//후에 데미지로 변경
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
        IsDamaged = false;
    }

}
