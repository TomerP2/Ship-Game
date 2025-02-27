using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class enemyController : MonoBehaviour
{
    private ShipController shipController;
    private GameObject player;

    void Start()
    {
        shipController = GetComponent<ShipController>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // Move forward in a straight line
        shipController.MoveShip(1, 0);

        // Get vector that points to player
        Vector2 towardsPlayer = (player.transform.position - transform.position).normalized;

        // Turn towards player
        shipController.RotateTowards(towardsPlayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile")) // If hit by cannonball.
        {
            Destroy(other.gameObject);
            shipController.DestroyShip();
        } else if (other.gameObject.CompareTag("Enemy")) // If hit other enemy.
        {
            shipController.DestroyShip();
        } else if (other.gameObject.CompareTag("Player")) // If hit player.
        {
            ShipController otherShip = other.gameObject.GetComponent<ShipController>();
            otherShip.DestroyShip();
            shipController.DestroyShip();
        }
    }
}
