using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCalculator : MonoBehaviour
{
    //five quiz grades
    //create a program that calculates random quiz grades and prints the average

    public float quiz1, quiz2, quiz3, quiz4, quiz5;

    
    // Start is called before the first frame update
    void Start()
    {

        quiz1 = Random.Range(0f, 100f);
        quiz2 = Random.Range(0f, 100f);
        quiz3 = Random.Range(0f, 100f);
        quiz4 = Random.Range(0f, 100f);
        quiz5 = Random.Range(0f, 100f);

        float averageQuiz = (quiz1 + quiz2 + quiz3 + quiz4 + quiz5) / 5;

        // making to 2 decimal points

        averageQuiz = Mathf.Round(averageQuiz * 100f)/ 100f;

        Debug.Log("The average quiz grade is: " + averageQuiz);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
