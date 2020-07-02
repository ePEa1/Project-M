using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterFSMPlayer : MonsterFSMBase
{

    [Header("UI")]
    [SerializeField] GameObject hpBox;
    [SerializeField] Image backhp;
    [SerializeField] Image hp;

    [Header("SeObject")]
    public Transform player;
    public PlayerFsmManager playerfsm;


    [Header("MonsterState")]
    public float currentHP = 50;
    [SerializeField] float maxHP = 50;
    [SerializeField] float attackRange = 4.5f; // 공격범위
    [SerializeField] float moveSpeed;
    [SerializeField] public float rotateSpeed;
    [SerializeField] float restTime = 5f;


    [Header("OtherValue")]
    public Vector3 destination;
    public Vector3 diff;
    public bool IsChase = false;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerfsm = player.GetComponent<PlayerFsmManager>();
    }


    void Update()
    {
        hpBox.transform.rotation = Camera.main.transform.rotation;
        hp.fillAmount = currentHP / maxHP;
        if (IsChase)
        {
            SetState(MonsterState.Chase);
        }
    }
    protected override IEnumerator Idle()
    {
        float t_idle = 0;
        do
        {
            yield return null;
            t_idle += Time.deltaTime;
            if(t_idle >= restTime)
            {
                SetState(MonsterState.Move);
            }
            if (Util.Detect(transform.position, player.transform.position, 5))
            {
                SetState(MonsterState.Chase);
                break;
            }
        } while (!isNewState);
    }

    protected virtual IEnumerator Move()
    {
        destination = new Vector3((transform.position.x + Random.Range(-5, 5)), transform.position.y, (transform.position.z + Random.Range(-5, 5)));

        do
        {
            yield return null;
            if (Util.Detect(transform.position, player.transform.position, 5))
            {
                SetState(MonsterState.Chase);
                break;
            }
            Util.CKMove(transform.gameObject, transform, destination, moveSpeed, rotateSpeed);
            diff = destination - transform.position;

            if (diff.sqrMagnitude < 0.3f * 0.3f)
            {
                SetState(MonsterState.Idle);
                break;
            }

        } while (!isNewState);
    }
    protected virtual IEnumerator Chase()
    {
        IsChase = true;
        Vector3 destination = player.position;
        Util.CKMove(transform.gameObject, transform, destination, moveSpeed, rotateSpeed);

        do
        {
            Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
            Vector3 diff = destination - destinationposition;
            Vector3 groundCheck = diff - destination;

            if (groundCheck.sqrMagnitude <= attackRange * attackRange)
            {
                IsChase = false;

                SetState(MonsterState.Attack);
                break;
            }

            yield return null;
        } while (!isNewState);
    }


    protected virtual IEnumerator Attack()
    {

        do
        {
            yield return null;


        } while (!isNewState);
    }

    protected virtual IEnumerator Dead()
    {
        do
        {
            yield return null;
            Destroy(gameObject, 2);
        } while (!isNewState);
    }
    public void GetDamage(AtkCollider dam)
    {
        currentHP -= dam.atkDamage;

        if(currentHP <= 0)
        {
            SetState(MonsterState.Dead);
            currentHP = 0;
            return;
        }
        if(CHState != MonsterState.Attack)
        {
            Util.CKRotate(transform, player.position, rotateSpeed);
        }
    }
}
