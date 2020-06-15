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

    [SerializeField] float m_hpMaxSize;
    float playerHP;

    bool HPDecrease = false;

    // Start is called before the first frame update
    void Start()
    {
        //HP = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        //m_hpMaxSize = transform.GetComponent<PlayerFsmManager>().MaxHP;
        HP.GetComponent<Image>().fillAmount = 1;
        BackHP.GetComponent<Image>().fillAmount = 1;
      

    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = PlayerStats.playerStat.m_currentHp+ "/" + PlayerStats.playerStat.m_maxHp;

        HP.GetComponent<Image>().fillAmount = PlayerStats.playerStat.m_currentHp / PlayerStats.playerStat.m_maxHp;
        if (HPDecrease)
        {
            BackHP.GetComponent<Image>().fillAmount = Mathf.Lerp(BackHP.fillAmount, HP.fillAmount, Time.deltaTime * 10f);
            if (HP.fillAmount >= BackHP.fillAmount - 0.01f)
            {
                HPDecrease = false;
                BackHP.fillAmount = HP.fillAmount;
            }
        }


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
