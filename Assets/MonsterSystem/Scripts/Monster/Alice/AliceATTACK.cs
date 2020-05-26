using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceATTACK : MonoBehaviour
{

    public Collider OneCloseAtkCol;
    public Collider TwoClosetAtkCol;


    // Start is called before the first frame update
    void Start()
    {
        OneCloseAtkCol = transform.GetChild(3).GetChild(0).GetComponent<Collider>();
        TwoClosetAtkCol = transform.GetChild(3).GetChild(1).GetComponent<Collider>();
        OneCloseAtkCol.enabled = false;
        TwoClosetAtkCol.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOneCloseAtk()
    {
        StartCoroutine(SetOne());
    }
    public void SetTwoCloseAtk()
    {
        StartCoroutine(SetTwo());
    }

    IEnumerator SetOne()
    {

        OneCloseAtkCol.enabled = true;
        yield return new WaitForSeconds(0.3f);
        OneCloseAtkCol.enabled = false;


    }

    IEnumerator SetTwo()
    {
        TwoClosetAtkCol.enabled = true;
        yield return new WaitForSeconds(0.3f);
        TwoClosetAtkCol.enabled = false;

    }
}
