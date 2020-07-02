using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProjectM.ePEa.CustomFunctions.CustomFunction;
using System;

public class MonsterDAMAGED : MonsterFSMState, DamageModel
{
    public MonsterFSMManager manager;
    [SerializeField] SkinnedMeshRenderer[] monsterRenderer;
    [SerializeField] AnimationCurve m_damAc;
    public GameObject m_damSfx;
    [SerializeField] Material[] originalMat;//원래 마테리얼
    [SerializeField] Material damageMat;//피격 마테리얼
    AtkCollider damInfo;//받아오는 콜리더


    public bool IsDamaged;//데미지 상태

    [Header("Vectors")]
    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_knockStart;
    Vector3 m_knockEnd;

    float m_knockTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        //manager = GetComponentInParent<MonsterFSMManager>();
        //DamageSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //피가 0일때 죽음
        if(manager.stat.hp <= 0)
        {
            manager.SetDead();
        }
    }

    //충돌 판정
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PCAtkCollider")
        {
            Debug.Log("Check");
            Damage();
            TakeDamage(other.GetComponent<AtkCollider>().atkDamage, other.GetComponent<AtkCollider>().knockVec, other.GetComponent<AtkCollider>().knockPower);
            if (other.GetComponent<AtkCollider>().AtkEvent())
            {
                transform.GetComponent<AudioSource>().volume = DataController.Instance.gameData.EffectSound;
                DataController.Instance.SetCombo();
                GameObject sfx = Instantiate(manager.m_damEff);
                sfx.transform.position = manager.transform.position;
            }
                //damInfo = other.GetComponent<AtkCollider>();

                //manager.anim.Rebind();
                //manager.anim.Play("DAMAGE");

                //if (damInfo.AtkEvent())
                //    StartCoroutine(StopMoment());
                //DamageSound.Play();

                //IsDamageCheck();

                //KnockBack();
            }
    }

    public void TakeDamage(float damage, Vector3 knockDir, float knockPower)
    {
        manager.stat.currentHp -= damage;

        manager.stat.m_time = 0.0f;

        manager.stat.m_currentReTime = manager.stat.m_refillTime;


        m_knockTime = 0;
        m_knockStart = new Vector3(transform.position.x, 0.0f, transform.position.z);
        m_knockEnd = m_knockStart + knockDir * knockPower;
        transform.rotation = Quaternion.LookRotation(-knockDir);
        GameObject eff = Instantiate(manager.m_damEff);

        eff.transform.position = transform.position + Vector3.up;

        Invoke("HPDecrease", 0.5f);

    }

    void Damage()
    {
        StartCoroutine(IsDamage());

        Vector3 before = Vector3.Lerp(m_knockStart, m_knockEnd, m_damAc.Evaluate(m_knockTime));
        m_knockTime += Time.deltaTime * 3.5f;
        Vector3 after = Vector3.Lerp(m_knockStart, m_knockEnd, m_damAc.Evaluate(m_knockTime));

        Vector3 fixedPos = FixedMovePos(transform.position, 0.6f, (after - before).normalized, Vector3.Distance(before, after), manager.m_wall);

        transform.position += after - before + fixedPos;

        if (m_knockTime > 1)
        {
            manager.SetState(MonsterState.IDLE);
        }
    }

    void HPDecrease()
    {
        manager.stat.IsDecrease = true;
    }

    //맞을 때 일시정지 하는 코루틴
    IEnumerator StopMoment()
    {
        manager.anim.Play("DAMAGE");
        yield return new WaitForSeconds(0.2f);
        manager.anim.speed = 0;
        yield return new WaitForSeconds(0.5f);
        manager.anim.speed = 1;

    }
    //데미지 입을때 마테리얼 변경
    IEnumerator IsDamage()
    {
        for(int i = 0; i<monsterRenderer.Length; i++)
        {
            monsterRenderer[i].material = damageMat;
        }
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i<monsterRenderer.Length; i++)
        {
            monsterRenderer[i].material = originalMat[i];
        }
    }

    //데미지 이펙트 생성
    void IsDamageCheck()
    {
        Debug.Log("Isdamage");
        IsDamaged = true;
        //GameObject curhiteff = Instantiate(hiteff);
        //curhiteff.transform.position = transform.position;
    }


    //void enemyknockback()
    //{
    //    manager.transform.position = manager.transform.position + new Vector3(0, 0, 5);
    //}

    //넉백 함수
    void KnockBack()
    {
        Debug.Log("startcorutine");
        Vector3 knockbackPos = manager.transform.position + damInfo.knockVec * damInfo.knockPower;
        manager.transform.position = knockbackPos;
        manager.stat.hp -= 10;
        IsDamaged = false;
    }

    public void TakeDamage(AtkCollider dam)
    {
        TakeDamage(dam.atkDamage, dam.knockVec, dam.knockPower);
        if (dam.GetComponent<AtkCollider>().AtkEvent())
        {
            //transform.GetComponent<AudioSource>().volume = DataController.Instance.gameData.EffectSound;
            DataController.Instance.SetCombo();
            GameObject sfx = Instantiate(m_damSfx);
            sfx.transform.position = manager.transform.position;
        }
    }
}
