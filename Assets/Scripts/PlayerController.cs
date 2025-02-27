using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ShipController shipController;
    public GameObject cannonball;
    public int cannonsDelay = 5;
    private bool rightCannonsReady = true;
    private bool leftCannonsReady = true;
    private float cannonballHorizontalSpawnOffset = 0.4f; // How far from the ship the cannonball spawns horizontally.

    void Start()
    {
        shipController = GetComponent<ShipController>();        
    }

    void Update()
    {
        shipController.MoveShip(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.E)) {shootCannonball("Right");}
        else if (Input.GetKeyDown(KeyCode.Q)) {shootCannonball("Left");}
    }

    private void shootCannonball(string direction)
    {
        Vector3 pos;
        Quaternion rotation;

        rotation = transform.rotation;
        pos = transform.position;

        if (direction == "Right" && rightCannonsReady)
        {
            pos += transform.right * cannonballHorizontalSpawnOffset;
            Instantiate(cannonball, pos, rotation);

            // Reenable cannons after x seconds.
            rightCannonsReady = false;
            StartCoroutine(activateActionAfter(cannonsDelay, () => rightCannonsReady = true));
        } 
        else if (direction == "Left" && leftCannonsReady)
        {
            pos -= transform.right * cannonballHorizontalSpawnOffset;
            rotation *= Quaternion.Euler(0, 180, 0);
            Instantiate(cannonball, pos, rotation);

            // Reenable cannons after x seconds.
            leftCannonsReady = false;
            StartCoroutine(activateActionAfter(cannonsDelay, () => leftCannonsReady = true));
        }
    }

    IEnumerator activateActionAfter(int seconds, Action setter)
    {
        yield return new WaitForSeconds(seconds);
        setter();
    }
}
