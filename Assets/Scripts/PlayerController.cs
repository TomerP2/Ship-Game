using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ShipController shipController;
    public GameObject cannonball;

    void Start()
    {
        shipController = GetComponent<ShipController>();        
    }

    void Update()
    {
        shipController.moveShip(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.E)) {shootCannonball("Right");}
        else if (Input.GetKeyDown(KeyCode.Q)) {shootCannonball("Left");}
    }

    private void shootCannonball(string direction)
    {
        Vector3 pos;
        Quaternion rotation;

        rotation = transform.rotation;
        pos = transform.position;

        if (direction == "Right")
        {
            pos += transform.right * 0.7f;
        } else 
        {
            pos -= transform.right * 0.7f;
            rotation *= Quaternion.Euler(0, 180, 0);
        }
        Instantiate(cannonball, pos, rotation);
    }
}
