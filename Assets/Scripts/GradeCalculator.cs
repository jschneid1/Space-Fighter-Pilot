using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeCalculator : MonoBehaviour
{
    //5 quiz results
    //calculate average of results
    //print out grade based on average result

        //print A =>= 90
        //print B =>= 80 < 90
        //print C =>= 70 < 80
        //print F =< 70
    public float result1, result2, result3, result4, result5;
    public float average;

    // Start is called before the first frame update
    void Start()
    {
        //result1 = 90.0f;//Random.Range(0f, 100f);
        //result2 = 90.0f; //Random.Range(0f, 100f);
        //result3 = 90.0f; // Random.Range(0f, 100f);
        //result4 = 90.0f; // Random.Range(0f, 100f);
        //result5 = 89.0f; // Random.Range(0f, 100f);

        average = (result1 + result2 + result3 + result4 + result5) / 5;

        if (average > 89.99f)
        {
            Debug.Log("The average grade is: A!!!");
        }

        else if ((average > 79.99f) && (average < 90))
        {
            Debug.Log("The average grade is: B!!");
        }

        else if ((average > 69.99f) && (average < 80))
        {
            Debug.Log("The average grade is: C!");
        }

        else
        {
            Debug.Log("The average grade is: F :(");
        }

    }

   
    // Update is called once per frame
    void Update()
    {

        
    }
}
