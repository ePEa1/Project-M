using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTime : MonoBehaviour
{
    [SerializeField] float m_lifeTime; //생존 시간

    private void Start()
    {
        Destroy(gameObject, m_lifeTime);

    }
}
