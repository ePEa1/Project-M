using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFsmManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] string m_startStat; //시작 시 상태
    [SerializeField] Animator m_animator; //애니메이터

    #endregion

    #region Value

    public string[] m_stats { get; private set; } //상태 모음
    [SerializeField] Dictionary<string, EnemyAction> m_actions; //상태에 따른 액션 모음

    public string m_currentStat { get; private set; } //현재상태
    EnemyAction m_currentAction; //현재상태에 따른 실행액션

    public PlayerFsmManager m_player { get; private set; }
    public Animator m_currentAni { get { return m_animator; } }
    bool m_setupOk = false;
    bool m_start = false;
    #endregion


    #region Functions

    /// <summary>
    /// fsm 셋팅 함수 / 셋팅 성공여부 반환
    /// </summary>
    /// <returns></returns>
    bool Setup()
    {
        m_currentStat = "";

        if (GameObject.FindWithTag("Player") != null)
            m_player = GameObject.FindWithTag("Player").GetComponent<PlayerFsmManager>();
        else
        {
            Debug.LogError(gameObject.name + " : 적이 탐지할 플레이어 캐릭터가 존재하지 않습니다");
            return false;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform obj = transform.GetChild(i);
            if (obj.name=="Events")
            {
                m_stats = new string[obj.childCount];
                m_actions = new Dictionary<string, EnemyAction>();
                if (m_stats.Length > 0)
                {
                    for (int j = 0; j < obj.childCount; j++)
                    {
                        m_stats[j] = obj.GetChild(j).name;
                        if (obj.GetChild(j).GetComponent<EnemyAction>() == null)
                        {
                            Debug.LogError(gameObject.name + ".Events." + obj.GetChild(j).name + " : 해당 오브젝트에 액션스크립트가 존재하지않습니다");
                            return false;
                        }
                        else
                        {
                            EnemyAction act = obj.GetChild(j).GetComponent<EnemyAction>();
                            m_actions.Add(m_stats[j], act);
                        }
                    }

                    m_currentAction = m_actions[m_startStat];
                    
                    return true;
                }
                else Debug.LogError(gameObject.name + " : 이벤트 오브젝트에 액션 오브젝트가 없습니다. Events 하위에 오브젝트 생성후, 액션스크립트를 넣어주십시오");
                
                return false;
            }
        }
        Debug.LogError(gameObject.name + " : 이벤트 오브젝트가 없습니다. 자식 오브젝트로 Events 생성 후, 그 하위에 액션 오브젝트를 만들어주십시오");
        return false;
    }

    #endregion

    #region Events

    void Awake()
    {
        m_setupOk = Setup();
    }

    void Update()
    {
        if (m_setupOk)
        {
            if (!m_start)
            {
                m_start = true;
                m_currentAction.OnStartAction();
            }
            else
                m_currentAction.OnUpdateAction();
        }
    }

    /// <summary>
    /// 상태 변경 이벤트
    /// </summary>
    /// <param name="act">변경할 이벤트 이름</param>
    public void ChangeStat(string act)
    {
        if (act != m_currentStat)
        {
            m_currentAction.OnEndAction(); //액션 종료 함수 실행
            m_currentStat = act; //현재상태 변경
            m_currentAction = m_actions[act]; //현재 액션 변경
            m_currentAction.OnStartAction(); //액션 시작 함수 실행
        }
    }

    #endregion
}
