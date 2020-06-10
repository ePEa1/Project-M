using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShakeManager : MonoBehaviour
{
    [SerializeField] public float shakeAmount;//쉐이크 최대 값
    [SerializeField] public float duration;//주기

    Vector3 originPos;

    void Start()
    {

    }

    void FixedUpdate()
    {
        originPos = transform.position;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Shake(shakeAmount, duration));
        }
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;

    }
}
