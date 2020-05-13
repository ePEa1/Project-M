using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEffController : MonoBehaviour
{
    public bool nowhit = false;
    public bool noweffrun = false;

    public GameObject hitEff;

    public AudioSource hitsound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nowhit)
        {
            StartCoroutine("Hit_Eff");
            hitsound.Play();
            nowhit = false;
        }
    }


    IEnumerator Hit_Eff()
    {

        if (!noweffrun)
        {
            noweffrun = true;
            hitEff.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            hitEff.SetActive(false);
            noweffrun = false;
        }


    }
}
