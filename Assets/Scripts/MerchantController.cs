using UnityEngine;

public class MerchantController : MonoBehaviour
{
    private ShipController shipController;

    void Start()
    {
        shipController = GetComponent<ShipController>();
    }

    void Update()
    {
        // Move forward in a straight line
        shipController.MoveShip(1, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
