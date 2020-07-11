using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    static QuestManager m_manager;
    public static QuestManager questManager { get { return m_manager; } }

    public enum QuestType
    {
        EnemyKill,
        Move,
        LoadEnemyKill,
        BossKill
    }

    [SerializeField] string[] m_quests;

    private void Awake()
    {
        m_manager = this;
    }

    public void SetQuest(QuestType t)
    {
        transform.GetChild(0).GetComponent<Text>().text = m_quests[(int)t];
        DataController.Instance.gameData.QuestOrder = (int)t;
    }

    public void StartQuest(int i)
    {
        transform.GetChild(0).GetComponent<Text>().text = m_quests[i];

    }
}
