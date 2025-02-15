using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float minSecsBetweenTurns = 30;
    public float maxSecsBetweenTurns = 60;
    private ShipController shipController;

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
