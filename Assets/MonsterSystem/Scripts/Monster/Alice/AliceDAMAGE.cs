using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AliceDAMAGE : AliceFSMState
{
    public GameObject hiteff;
    public SkinnedMeshRenderer skinMat;
    public Material damageMat;
    public Material OriginalMat;
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
            damInfo = other.GetComponent<AtkCollider>();
            //manager.anim.Rebind();
            //manager.anim.Play("DAMAGE");
            IsDamageCheck(damInfo.atkDamage);
            if (damInfo.AtkEvent())
                manager.DamageSound.Play();
            StartCoroutine(Damage());
            KnockBack();
        }
    }

    IEnumerator Damage()
    {
        skinMat.material = damageMat;
        yield return new WaitForSeconds(0.2f);
        skinMat.material = OriginalMat;


    }
    void IsDamageCheck(float damage)
    {
        CreatHitEff();
        IsDamaged = true;
        manager.CurAliceHP -= damage; //후에 데미지로 변경
        HpManager.ChangeHp(manager.CurAliceHP);
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
