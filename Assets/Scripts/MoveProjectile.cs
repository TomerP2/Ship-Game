using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    private float distance = 0;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        distance += speed * Time.deltaTime;
        if (distance > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
