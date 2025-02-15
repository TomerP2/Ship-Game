using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float minSecsBetweenTurns = 30;
    public float maxSecsBetweenTurns = 60;

    private ShipController shipController;
    private float nextTriggerTime = 0;
    private bool isTurning = false;

    void Start()
    {
        shipController = GetComponent<ShipController>();
    }

    void Update()
    {
        // Move forward in a straight line
        shipController.moveShip(1, 0);
        
        // Make a turn once in while
        if (Time.time >= nextTriggerTime && !isTurning)
        {
            turnShip();
        }
    }
    private void turnShip()
    {
        Debug.Log("Turning!");
        nextTriggerTime = Time.time + Random.Range(minSecsBetweenTurns, maxSecsBetweenTurns);
        isTurning = true;
        float angle = Random.Range(-1f, 1f);
        for (int i = 0; i < 10; i++) { 
            shipController.moveShip(0, angle); 
        }
    }
}
