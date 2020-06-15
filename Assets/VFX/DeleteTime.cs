using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTime : MonoBehaviour
{
    [SerializeField] float m_lifeTime; //생존 시간

    void Start()
    {
        Destroy(gameObject,m_lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.GetComponent<AudioSource>().volume = DataController.Instance.gameData.EffectSound;
    }
}
