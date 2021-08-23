using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField]
    private int _score;

    private bool _messageWasSaid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if we hit the space key
        //add 10 points
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //score = score + 10;
            _score += 10;
                       
        }

        //if points is greater than or equal to 50 AND && messageWasSaid
        //print out you are awesome!

        if (_score >= 50 && _messageWasSaid == false)
        {
            Debug.Log("You are awesome!");
            _messageWasSaid = true;
        }
    }
}
