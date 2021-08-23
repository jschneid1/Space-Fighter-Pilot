using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TrialRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    IEnumerator TrialRoutine()
    {
        while (true)
        {
            Debug.Log("Waiting for 2 seconds");
            yield return new WaitForSeconds(2.0f);
         }
    }
    
        

}
