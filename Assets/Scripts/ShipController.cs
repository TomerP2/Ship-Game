using System;
using System.ComponentModel;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // Private variables
    private float speed = 0;
    private float rudderAngle = 0;

    // Public variables
    public float acceleration = 1;
    public float maxSpeed = 3;
    public float minSpeed = -1;
    public float turnSpeed = 5;
    public float rudderAcceleration = 1;
    public float rudderRange = 3;

    void Start() {}

    void Update() {}
    public void moveShip(float verticalInput, float horizontalInput)
    {
        // Test for vertical/horizontal input limits
        if ( !(-1 <= verticalInput || verticalInput >= 1 || -1 <= horizontalInput || horizontalInput >= 1) )
         {
            Debug.Log("Vertical input:" + verticalInput + ". Horizontal input:" + horizontalInput);
            throw new ArgumentOutOfRangeException("Input is out of range"); 
        }

        // Handle ship speed
        speed += acceleration * verticalInput * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;
        if (speed < minSpeed) speed = minSpeed;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Handle ship rotation
        rudderAngle += horizontalInput * Time.deltaTime * rudderAcceleration;
        if (rudderAngle < -rudderRange) rudderAngle = -rudderRange;
        if (rudderAngle > rudderRange) rudderAngle = rudderRange;

        transform.Rotate(Vector3.up * turnSpeed * rudderAngle * Time.deltaTime);
    }

    public void straightenRudder() {
        while (rudderAngle != 0) {
            if (rudderAngle < 0) { moveShip(0, 1);}
            else { moveShip(0, -1);}
        }
    }


}
