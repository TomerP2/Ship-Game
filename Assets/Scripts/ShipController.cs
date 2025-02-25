using System;
using System.ComponentModel;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // Private variables
    private float speed = 0;
    private float rudderAngle = 0;
    private float decreaseRudderAngle = 180; // when the angle between the enemys current direction and the player is less than this, the rudder angle will be decreased.

    // Public variables
    public float acceleration = 1;
    public float maxSpeed = 3;
    public float minSpeed = -1;
    public float turnSpeed = 5;
    public float rudderAcceleration = 1;
    public float rudderRange = 3;

    void Start() {}

    void Update() {
    }

    public void MoveShip(float verticalInput, float horizontalInput)
    {
        Debug.Log(horizontalInput);
        // Test for vertical/horizontal input limits
        if (-1 > verticalInput || verticalInput > 1 || -1 > horizontalInput || horizontalInput > 1)
         {
            Debug.Log("Vertical input:" + verticalInput + ". Horizontal input:" + horizontalInput);
            throw new ArgumentOutOfRangeException("Input is out of range"); 
        }

        // Handle ship speed
        speed += acceleration * verticalInput * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;
        if (speed < minSpeed) speed = minSpeed;

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Handle ship rotation
        rudderAngle += horizontalInput * Time.deltaTime * rudderAcceleration;
        if (rudderAngle < -rudderRange) rudderAngle = -rudderRange;
        if (rudderAngle > rudderRange) rudderAngle = rudderRange;

        transform.Rotate(-1 * Vector3.forward * turnSpeed * rudderAngle * Time.deltaTime);
    }

    public void RotateTowards(Vector2 direction)
    {
        Debug.DrawRay(transform.position, transform.up, Color.white);
        // Get degrees between the ships forward direction and the vector towards the player. 
        float degrees = Vector2.SignedAngle(transform.up, direction);
        float horizontalInput = ScaleDegreesToRange(degrees);
        MoveShip(0, horizontalInput);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle collisions with border
        if (other.gameObject.CompareTag("Border"))
        {
            // Get normal of border
            Vector2 normal = -1 * other.gameObject.transform.up;
            // Get vector of reflection of ship from border
            Vector2 reflection = Vector3.Reflect(transform.up, normal);
            // Get angle of reflection vector
            float angle = (Mathf.Atan2(reflection.y, reflection.x) * Mathf.Rad2Deg) - 90f;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private float ScaleDegreesToRange(float degrees)
    {
        // Clamp degrees between -180 and 180
        if (degrees < -decreaseRudderAngle)
        {
            return 1f;
        }
        else if (degrees >= -decreaseRudderAngle && degrees <= decreaseRudderAngle) 
        {
            return -1 * Mathf.Lerp(-1f, 1f, (degrees + decreaseRudderAngle) / (2 * decreaseRudderAngle)); 
        }
        else
        {
            return -1f; 
        }
    }
}
