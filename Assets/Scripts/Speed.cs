using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{

    //create a program that lets you increment the speed when hit S key
    //decrement speed when hit A
    //when speed > 20 print out slow down
    //when speed = 0 print out speed up
    //make so speed can't go below 0

    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            speed = speed + 4;
            //speed += 4;
            //best practice is to use speed += 4;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            speed = speed - 4;
            //best practice to use speed -= 4;
        }

        if(speed <= 0)
        {
            speed = 0;
            Debug.Log("You need to speed up!");
        }
        else if(speed > 20)
        {
            Debug.Log("You need to slow down!!!");
        }
    }
}
