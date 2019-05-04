using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public VirtualJoyStick JoyStick; //created referece to our VirtualJoyStick script 
    private float speed = 10f;
   

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.right * JoyStick.Horizontal() * speed * Time.deltaTime);//referenced two methods using Joystick as a handle to the script
        transform.Translate(Vector3.forward * JoyStick.Vertical() * speed * Time.deltaTime);
    }
}
