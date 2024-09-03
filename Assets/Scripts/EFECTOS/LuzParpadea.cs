using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzParpadea : MonoBehaviour
{
    Light LinternaMenu;
    public float minWaitTime;
    public float maxWaitTime;

    private void Start()
    {
        LinternaMenu = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            LinternaMenu.enabled = !LinternaMenu.enabled;    
        }
    }
}
