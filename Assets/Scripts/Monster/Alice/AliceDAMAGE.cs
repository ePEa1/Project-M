using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AliceDAMAGE : AliceFSMState
{
    public GameObject hiteff;
    public Image HPGauge;
    AtkCollider damInfo;

    public bool IsDamaged = false;
    public override void BeginState()
    {
        base.BeginState();
    }


    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<AliceFSMManager>();
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
            Debug.Log("Check");
            damInfo = other.GetComponent<AtkCollider>();
            manager.anim.SetInteger("curState", 3);
            IsDamageCheck();

            KnockBack();
        }
    }
    void IsDamageCheck()
    {
        Debug.Log("Isdamage");
        IsDamaged = true;
        manager.CurAliceHP -= 1;//후에 데미지로 변경
        HPGauge.fillAmount = manager.CurAliceHP;
        Debug.Log(manager.CurAliceHP);
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
