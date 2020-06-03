using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDAMAGED : MonsterFSMState
{
    public GameObject hiteff;//이펙트
    public AudioSource DamageSound;//사운드
    public MeshRenderer skinned;//피격 마테리얼 바꾸는 렌더러
    public Material originalMat;//원래 마테리얼
    public Material damageMat;//피격 마테리얼
    AtkCollider damInfo;//받아오는 콜리더
    public bool IsDamaged;//데미지 상태
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<MonsterFSMManager>();
        DamageSound = GetComponent<AudioSource>();
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
            
            damInfo = other.GetComponent<AtkCollider>();

            manager.anim.Rebind();
            manager.anim.Play("DAMAGE");

            if (damInfo.AtkEvent())
                StartCoroutine(StopMoment());
            DamageSound.Play();

            IsDamageCheck();

            StartCoroutine(Damage());
            KnockBack();
        }
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
    IEnumerator Damage()
    {
        skinned.material = damageMat;
        yield return new WaitForSeconds(0.1f);
        skinned.material = originalMat;


    }

    //데미지 이펙트 생성
    void IsDamageCheck()
    {
        Debug.Log("Isdamage");
        IsDamaged = true;
        GameObject curhiteff = Instantiate(hiteff);
        curhiteff.transform.position = transform.position;
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
}
