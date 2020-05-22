using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDAMAGED : MonsterFSMState
{
    public GameObject hiteff;
    public AudioSource DamageSound;
    AtkCollider damInfo;
    public int SetDamage;
    public bool IsDamaged;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<MonsterFSMManager>();
        DamageSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.stat.hp <= 0)
        {
            manager.SetDead();
        }
    }
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

            //StartCoroutine(KnockBack());
            KnockBack();
        }
    }

    IEnumerator StopMoment()
    {
        manager.anim.Play("DAMAGE");
        yield return new WaitForSeconds(0.2f);
        manager.anim.speed = 0;
        yield return new WaitForSeconds(0.5f);
        manager.anim.speed = 1;

    }
    void IsDamageCheck()
    {
        Debug.Log("Isdamage");
        IsDamaged = true;
        GameObject curhiteff = Instantiate(hiteff);
        curhiteff.transform.position = transform.position;
    }
    //IEnumerator KnockBack()
    //{
    //    Debug.Log("startcorutine");

    //    manager.transform.position += damInfo.knockVec * damInfo.knockPower;

    //    yield return new WaitForSeconds(0.3f);
    //    IsDamaged = false;
    //}
    void enemyknockback()
    {
        manager.transform.position = manager.transform.position + new Vector3(0, 0, 5);
    }

    void KnockBack()
    {
        Debug.Log("startcorutine");
        Vector3 knockbackPos = manager.transform.position + damInfo.knockVec * damInfo.knockPower;
        manager.transform.position = knockbackPos;
        manager.stat.hp -= 10;
        //yield return new WaitForSeconds(0.3f);
        IsDamaged = false;
    }
}
