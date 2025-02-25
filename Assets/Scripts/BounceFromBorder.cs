using UnityEngine;

public class BounceFromBorder : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 pos = transform.position;

        if (other.gameObject.CompareTag("Border")) {
            if (Mathf.Abs(pos.y) > Mathf.Abs(pos.x)) {
                transform.Rotate(0, 0, 180 - 2 * transform.eulerAngles.z);
            } else
            {
                transform.Rotate(0, 0, -2 * transform.eulerAngles.z);
            }
        }
    }

}
