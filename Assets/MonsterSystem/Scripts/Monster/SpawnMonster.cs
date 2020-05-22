using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public GameObject Monster;

    public bool IsSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(Monster, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(10.0f);
        }

    }
}
