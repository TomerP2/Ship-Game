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
    }
}
