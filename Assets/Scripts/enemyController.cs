using UnityEngine;

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
        Debug.DrawRay(transform.position, towardsPlayer, Color.red);

        // Turn towards player
        shipController.RotateTowards(towardsPlayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile")) // If hit by cannonball.
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Player")) // If hit player.
        {
            Destroy(other.gameObject);
        }
    }
}
