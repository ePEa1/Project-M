using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ProjectM.ePEa.CustomFunctions.CustomFunction;


public class MonsterFSMPlayer : MonsterFSMBase
{

    [Header("UI")]
    [SerializeField] GameObject hpBox;
    [SerializeField] Image backhp;
    [SerializeField] Image hpbar;
    [SerializeField] Image refillBar;

    [Header("SeObject")]
    public Transform player;
    public PlayerFsmManager playerfsm;
    [SerializeField] AtkCollider m_atkCollider;
    [SerializeField] GameObject m_damEff;
    [SerializeField] GameObject m_atkeff;
    [SerializeField] LayerMask m_wall;
    [SerializeField] SkinnedMeshRenderer[] skinned;
    [SerializeField] Material[] originalMat;
    [SerializeField] Material damageMat;


    [Header("MonsterState")]
    public float currentHP = 50;
    [SerializeField] float maxHP = 50;
    [SerializeField] float attackRange = 4.5f; // 공격범위
    [SerializeField] float attackTime = 2f; // 공격범위
    [SerializeField] float m_rushRange = 2f; // 공격범위
    [SerializeField] float m_chaseRange = 8;
    [SerializeField] float moveSpeed;
    [SerializeField] float rushSpeed;
    [SerializeField] float refillSpeed;
    [SerializeField] public float rotateSpeed;
    [SerializeField] float restTime = 5f;
    [SerializeField] float atkrestTime = 3f;
    [SerializeField] float refillTime = 2;
    [SerializeField] AnimationCurve m_damAc;
    [SerializeField] AnimationCurve m_atkAc;
    public float currefillTime = 0;
    public float curattackTime = 0;

    float m_knockTime = 0;
    Vector3 m_knockStart;
    Vector3 m_knockEnd;


    [Header("OtherValue")]
    [SerializeField] float m_colliderOpenTime;
    [SerializeField] float m_backRange; //후퇴 거리
    [SerializeField] float m_atkDelay; //공격 딜레이
    public Vector3 destination;
    public Vector3 diff;
    Vector3 StartPos;
    Vector3 m_destPos;
    Vector3 m_startPos;
    Vector3 m_endPos;

    float m_time = 0.0f;
    float m_nowDelay = 0.0f;
    float m_attackDelay = 0f;

    public bool IsChase = false;
    public bool IsDecrease = false;
    public bool IsCombat = false;
    public bool Isalert = false;
    bool isAtk = false;


    protected override void Awake()
    {
        base.Awake();
        AddTarget();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerfsm = player.GetComponent<PlayerFsmManager>();
    }
    public void Start()
    {
        StartPos = transform.position;
    }

    void Update()
    {
        hpBox.transform.rotation = Camera.main.transform.rotation;
        hpbar.fillAmount = currentHP / maxHP;
        if (IsChase)
        {
            SetState(MonsterState.Chase);
        }
        if(CHState == MonsterState.Damage)
        {
            DamageStart();
        }
        if (CHState == MonsterState.Attack)
        {
            Atk();
        }

        currefillTime = Mathf.Max(0.0f, currefillTime - Time.deltaTime);
        if (currefillTime <= 0)
            currentHP = Mathf.Min(maxHP, currentHP + Time.deltaTime * refillSpeed);
        m_nowDelay = Mathf.Max(0, m_nowDelay - Time.deltaTime);

        refillBar.fillAmount = (refillTime-currefillTime) / refillTime;

        if (currentHP <= 0)
        {
            DataController.Instance.gameData.firstStageMonster -= 1;
            DestroyTarget();
            SetState(MonsterState.Dead);
        }
        //if (currentHP < 40)
        //{
        //    Isalert = true;
        //    SetState(MonsterState.Move);
        //}
        if (IsDecrease)
        {
            backhp.GetComponent<Image>().fillAmount = Mathf.Lerp(backhp.fillAmount, hpbar.fillAmount, Time.deltaTime * 5.0f);
            if (hpbar.fillAmount >= backhp.fillAmount - 0.01f)
            {
                IsDecrease = false;
                backhp.fillAmount = hpbar.fillAmount;

            }
        }
        if(IsCombat == true)
        {
            //Util.CKRotate(transform, player.position, rotateSpeed);
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
                    break;
                
            }
            if(t_idle >= atkrestTime)
            {
                if (IsCombat == true)
                {
                    SetState(MonsterState.Chase);
                    break;
                }
            }
            if (Util.Detect(transform.position, player.transform.position, m_chaseRange) && IsCombat ==false)
            {
                IsCombat = true;
                SetState(MonsterState.Chase);
                break;
            }
        } while (!isNewState);
    }

    protected virtual IEnumerator Move()
    {
        if (Isalert == true)
        {
            Isalert = false;
            destination = new Vector3((StartPos.x + Random.Range(-10, 10)), transform.position.y, (StartPos.z + Random.Range(-10, 10)));
        }
        else
        {
            destination = new Vector3((StartPos.x + Random.Range(-5, 5)), transform.position.y, (StartPos.z + Random.Range(-5, 5)));
        }

        do
        {
            yield return null;
            if (Util.Detect(transform.position, player.transform.position, m_chaseRange))
            {

                IsCombat = true;
                SetState(MonsterState.Chase);
                break;
            }
            Util.CKMove(transform.gameObject, transform, destination, moveSpeed, rotateSpeed);
           // Vector3 fixedPos = FixedMovePos(transform.position, 0.6f, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos), m_wall);

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

            if (groundCheck.sqrMagnitude <= attackRange* attackRange)
            {
                if(m_nowDelay <= 0)
                {
                    IsChase = false;
                    SetState(MonsterState.Attack);
                    break;
                }
                else
                {
                    SetState(MonsterState.Idle);
                    break;
                }
            }

            yield return null;
        } while (!isNewState);
    }


    protected virtual IEnumerator Attack()
    {
        AtkStart();

        do
        {
            yield return null;


        } while (!isNewState);
    }

    protected virtual IEnumerator Damage()
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
            Destroy(gameObject);
        } while (!isNewState);
    }

    protected virtual IEnumerator Alert()
    {
        do
        {
            yield return null;

        } while (!isNewState);
    }
    void Atk()
    {
        //Util.CKRotate(transform, player.position, rotateSpeed);

        float ac = 1.0f / rushSpeed;

        Vector3 beforePos = Vector3.Lerp(m_startPos, m_endPos, m_atkAc.Evaluate(m_time * ac));
        m_time += Time.deltaTime;
        Vector3 afterPos = Vector3.Lerp(m_startPos, m_endPos, m_atkAc.Evaluate(m_time * ac));

        Vector3 fixedPos = FixedMovePos(transform.position, 0.6f, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos), m_wall);

        if (!float.IsNaN(fixedPos.x))
            transform.position += afterPos - beforePos + fixedPos;
        else
            transform.position += afterPos - beforePos;

        if (m_time >= m_colliderOpenTime && !isAtk)
        {
            m_atkCollider.Attacking();
            isAtk = true;
        }

        if (m_time >= rushSpeed)
        {
            AtkEnd();
        }
    }

    void AtkStart()
    {
        isAtk = false;
        m_nowDelay = m_atkDelay;
        m_time = 0.0f;
        m_startPos = transform.position;
        m_endPos = transform.position + (player.position - transform.position).normalized * m_rushRange;

        GameObject eff = Instantiate(m_atkeff);
        eff.transform.parent = transform;
        eff.transform.localPosition = new Vector3(0.0f, 1.0f, 1.0f);
        //Util.CKRotate(transform, player.position, rotateSpeed);

        transform.rotation = Quaternion.LookRotation(m_endPos - m_startPos);
        m_atkCollider.knockPower = 5.0f;
        m_atkCollider.knockVec = (m_endPos - m_startPos).normalized;
    }

    public void AtkEnd()
    {
        SetState(MonsterState.Idle);
        m_time = 0.0f;

        m_destPos = new Vector3(transform.position.x, 0.0f, transform.position.z);

    }


    public void TakeDamage(float damage, Vector3 knockDir, float knockPower)
    {
        currentHP -= damage;
        Debug.Log("GetDamage");

        if (currentHP <= 0)
        {
            SetState(MonsterState.Dead);
            currentHP = 0;
            return;
        }
        //if (CHState != MonsterState.Attack)
        //{
        //    Util.CKRotate(transform, player.position, rotateSpeed);
        //}
        StartCoroutine(IsDamage());

       //m_time = 0.0f;

        currefillTime = refillTime;
        //m_knockTime = 0;
        //m_knockStart = new Vector3(transform.position.x, 0.0f, transform.position.z);
        //m_knockEnd = m_knockStart + knockDir * knockPower;
        //transform.rotation = Quaternion.LookRotation(-knockDir);
        GameObject eff = Instantiate(m_damEff);
        eff.transform.position = transform.position + Vector3.up;
        eff.transform.rotation = Camera.main.transform.rotation;

        Invoke("HPDecrease", 0.5f);

    }
    void DamageStart()
    {
        //Vector3 before = Vector3.Lerp(m_knockStart, m_knockEnd, m_damAc.Evaluate(m_knockTime));
        //m_knockTime += Time.deltaTime * 3.5f;
        //Vector3 after = Vector3.Lerp(m_knockStart, m_knockEnd, m_damAc.Evaluate(m_knockTime));

        //Vector3 fixedPos = FixedMovePos(transform.position, 0.6f, (after - before).normalized, Vector3.Distance(before, after), m_wall);

        //transform.position += after - before + fixedPos;

        //if (m_knockTime > 1)
        //{
        //    SetState(MonsterState.Idle);
        //}
    }
    IEnumerator IsDamage()
    {
        for (int i = 0; i < skinned.Length; i++)
        {
            skinned[i].material = damageMat;
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < skinned.Length; i++)
        {
            skinned[i].material = originalMat[i];
        }
    }

    void HPDecrease()
    {
        IsDecrease = true;
    }

    public void AddTarget()
    {
        EnemyRader rader = GameObject.FindWithTag("EnemyRader").GetComponent<EnemyRader>();
        if (rader != null)
        {
            rader.AddTarget(transform);
        }
    }

    public void DestroyTarget()
    {
        EnemyRader rader = GameObject.FindWithTag("EnemyRader").GetComponent<EnemyRader>();
        if (rader != null)
        {
            rader.DestroyTarget(transform);
        }
    }
}


