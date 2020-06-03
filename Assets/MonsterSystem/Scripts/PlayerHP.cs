using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Image HP;
    public Text HPText;
    public GameObject player;
    [SerializeField] float m_hpMaxSize;
    float playerHP;

    // Start is called before the first frame update
    void Start()
    {
        HP = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        //m_hpMaxSize = transform.GetComponent<PlayerFsmManager>().MaxHP;
        HP.GetComponent<Image>().fillAmount = 1;

    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = player.GetComponent<PlayerFsmManager>().HPcheck + "/" + m_hpMaxSize;
        Debug.Log(player.GetComponent<PlayerFsmManager>().HPcheck);
        HP.GetComponent<Image>().fillAmount = player.GetComponent<PlayerFsmManager>().HPcheck / m_hpMaxSize;

    }
}
