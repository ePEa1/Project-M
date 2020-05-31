using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AliceDAMAGE : AliceFSMState
{
    public GameObject hiteff;
    public IsDamagedEff[] ChildSkinned;
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
        //skinMat = GetComponent<SkinnedMeshRenderer>();
        manager = GetComponentInParent<AliceFSMManager>();
        HpManager = GameObject.FindGameObjectWithTag("HPGauge").GetComponent<EnemyHPViewManager>();
        HpManager.m_maxHp = manager.CurAliceHP;
        HpManager.Setup();
    }

    // Update is called once per frame
    public void Update()
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
            for(int i = 0; i<ChildSkinned.Length; i++)
            {
                ChildSkinned[i].CallDamageCoroutine();
            }
            damInfo = other.GetComponent<AtkCollider>();
            //manager.anim.Rebind();
            //manager.anim.Play("DAMAGE");
            IsDamageCheck(damInfo.atkDamage);
            if (damInfo.AtkEvent())
                manager.DamageSound.Play();

            //KnockBack();
        }
    }


    void IsDamageCheck(float damage)
    {
        CreatHitEff();
        IsDamaged = true;
        manager.CurAliceHP -= damage; //후에 데미지로 변경
        HpManager.ChangeHp(manager.CurAliceHP);
        if(manager.CurAliceHP <= 0)
        {
            manager.IsDead = true;
        }
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
