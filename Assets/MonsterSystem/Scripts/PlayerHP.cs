using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectM.ePEa.PlayerData;


using static ProjectM.ePEa.CustomFunctions.CustomFunction;


public class PlayerHP : MonoBehaviour
{
    public Image HP;
    public Image BackHP;
    public Text HPText;
    public GameObject player;

    [SerializeField] float m_blendSpeed;

    float m_hpMaxSize;
    float m_playerHp;

    bool HPDecrease = false;

    float m_blendHp;
    float m_blendTime;

    // Start is called before the first frame update
    void Start()
    {
        //HP = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");

        m_hpMaxSize = PlayerStats.playerStat.m_maxHp;
        m_playerHp = m_hpMaxSize;
        m_blendHp = m_hpMaxSize;

        HP.GetComponent<Image>().fillAmount = 1;
        BackHP.GetComponent<Image>().fillAmount = 1;
      

    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerHp != PlayerStats.playerStat.m_currentHp)
        {
            m_blendHp = m_playerHp / m_hpMaxSize;
            m_blendTime = 0;
            m_playerHp = PlayerStats.playerStat.m_currentHp;
        }

        m_blendTime = Mathf.Min(1, m_blendTime + Time.deltaTime * m_blendSpeed);
        BackHP.fillAmount = Mathf.Lerp(m_blendHp, m_playerHp / m_hpMaxSize, m_blendTime);

        HPText.text = m_playerHp + "/" + m_hpMaxSize;
        
        //HP.GetComponent<Image>().fillAmount = m_playerHp / m_hpMaxSize;
        //if (HPDecrease)
        //{
        //    BackHP.GetComponent<Image>().fillAmount = Mathf.Lerp(BackHP.fillAmount, HP.fillAmount, Time.deltaTime * 10f);
        //    if (HP.fillAmount >= BackHP.fillAmount - 0.01f)
        //    {
        //        HPDecrease = false;
        //        BackHP.fillAmount = HP.fillAmount;
        //    }
        //}
    }

    public void DamageDecrease()
    {
        Invoke("StartDecrese", 0.5f);
    }

    void StartDecrese()
    {
        HPDecrease = true;
    }
}
