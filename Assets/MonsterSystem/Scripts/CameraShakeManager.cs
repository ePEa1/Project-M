using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShakeManager : MonoBehaviour
{
    [SerializeField] public float shakeAmount;//쉐이크 최대 값
    [SerializeField] public float duration;//주기 0.05
    [SerializeField] public float addPos;//0.03
    [SerializeField] public float addRot;//0.1

    public bool shakeRotate = false;



    Quaternion originRot = Quaternion.identity;
    Vector3 originPos;
    void Start()
    {

    }

    void FixedUpdate()
    {


        if (Input.GetKeyDown(KeyCode.Z))
        {
            originPos = transform.position;
            originRot = transform.rotation;
            StartCoroutine(Shake(addPos, duration, addRot));
        }
    }

    public IEnumerator Shake(float _amount, float _duration, float _rotate)
    {

        float timer = 0;
        while (timer <= _duration)
        {
            //transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;
            Vector3 shakeRot = new Vector3(0,0, Mathf.PerlinNoise(Time.time * _rotate, 0.0f));

            transform.localRotation = transform.rotation *Quaternion.Euler(shakeRot);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
        transform.rotation = originRot;

    }
}
