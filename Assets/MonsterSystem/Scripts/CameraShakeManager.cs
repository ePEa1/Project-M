using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShakeManager : MonoBehaviour
{
    [SerializeField] public float shakeAmount;//쉐이크 최대 값
    [SerializeField] public float duration;//주기 0.05
    [SerializeField] public float addPos;//0.03 현재 로테이션만 적용되어있음
    [SerializeField] public float maxRot;//0.1
    [SerializeField] public float minRot;
    [SerializeField] public AnimationCurve RotCurve;

    public bool shakeRotate = false;



    Quaternion originRot = Quaternion.identity;
    Vector3 originPos;
    void Start()
    {

    }

    void FixedUpdate()
    {
        originPos = transform.position;
        originRot = transform.rotation;

        if (Input.GetKeyDown(KeyCode.Z))//이제 이걸 부딫힐때 하느냐
        {

            StartCoroutine(Shake(addPos, duration, maxRot));
        }
    }

    public IEnumerator Shake(float _amount, float _duration, float _rotate)
    {

        float timer = 0;
        float rottime = 0;
        while (timer <= _duration)
        {
            //transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;
            Vector3 shakeRot = new Vector3(0,0, Mathf.PerlinNoise(Time.time * _rotate, minRot));
            float shakeValue = Mathf.Lerp(minRot, _rotate, RotCurve.Evaluate(rottime));
            Vector3 curveresult = new Vector3(shakeValue,0, 0 );
            transform.localRotation = transform.rotation * Quaternion.Euler(curveresult);

            //transform.localRotation = transform.rotation *Quaternion.Euler(shakeRot);
            rottime += Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
        transform.rotation = originRot;

    }
}
