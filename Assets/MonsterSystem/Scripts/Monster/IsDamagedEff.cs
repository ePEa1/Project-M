using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDamagedEff : MonoBehaviour
{

    public SkinnedMeshRenderer skinned;
    public Material damageMat;
    public Material originalMat;

    // Start is called before the first frame update
    void Start()
    {
        skinned = GetComponent<SkinnedMeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallDamageCoroutine()
    {
        StartCoroutine(Damage());
    }
    IEnumerator Damage()
    {
        skinned.material = damageMat;
        yield return new WaitForSeconds(0.1f);
        skinned.material = originalMat;


    }
}
