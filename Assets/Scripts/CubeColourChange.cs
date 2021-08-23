using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColourChange : MonoBehaviour
{

    //create a program that when space hit increment a score value.
    //when score is greater than 50, turn cube green. Start cube colour red.

    public int score;
    public GameObject cube;

    // Start is called before the first frame update
    void Start()

    {
        cube.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            score = score + 5;
        }

        if(score > 50)
        {
            cube.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}
