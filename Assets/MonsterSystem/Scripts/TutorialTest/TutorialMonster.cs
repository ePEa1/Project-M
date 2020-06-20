using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMonster : MonoBehaviour
{

    public GameObject m_damSfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PCAtkCollider")
        {

                //transform.GetComponent<AudioSource>().volume = DataController.Instance.gameData.EffectSound;
                DataController.Instance.SetCombo();
                GameObject sfx = Instantiate(m_damSfx);
            
        }
    }
}
