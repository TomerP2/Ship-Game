using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipController : MonoBehaviour
{
    // Private variables
    private float speed = 0;
    private float rudderAngle = 0;
    private float decreaseTurningSpeed = 15; // when the angle between the enemys current direction and the player is less than this, the turning speed will be decreased.
    private GameController gameController;

    // Public variables
    public float acceleration = 1;
    public float maxSpeed = 3;
    public float minSpeed = -1;
    public float turnSpeed = 5;
    public float rudderAcceleration = 0.3f;
    public GameObject explosionPrefab;

    void Start() {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    void Update() {
    }

    public void MoveShip(float verticalInput, float horizontalInput)
    {
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
        if (rudderAngle < -1) rudderAngle = -1;
        if (rudderAngle > 1) rudderAngle = 1;
        rotateShip(rudderAngle);
    }

    public void RotateTowards(Vector2 direction)
    {
        // Get degrees between the ships forward direction and the vector towards the player. 
        float degrees = Vector2.SignedAngle(transform.up, direction);
        float angle = ScaleDegreesToRange(degrees);

        rotateShip(angle);
    }

    private void rotateShip(float rudderAngle)
    {
        transform.Rotate(-1 * Vector3.forward * turnSpeed * rudderAngle * Time.deltaTime);
    }

    public void DestroyShip()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.58f); // Adjust time based on animation length

        // Add to score if enemy is killed
        if (gameObject.CompareTag("Enemy"))
        {
            gameController.killCount++;
        }

        // End game if player is sunk.
        else if (gameObject.CompareTag("Player"))
        {
            gameController.GameOver();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle collisions with border
        if (other.gameObject.CompareTag("Border"))
        {
            DestroyShip();
        }
    }

    private float ScaleDegreesToRange(float degrees)
    {
        // Clamp degrees between -180 and 180
        if (degrees < -decreaseTurningSpeed)
        {
            return 1f;
        }
        else if (degrees >= -decreaseTurningSpeed && degrees <= decreaseTurningSpeed) 
        {
            return -1 * Mathf.Lerp(-1f, 1f, (degrees + decreaseTurningSpeed) / (2 * decreaseTurningSpeed)); 
        }
        else
        {
            return -1f; 
        }
    }
}
