using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 6f;
    public float destroyX = -15f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x <= destroyX)
            Destroy(gameObject);
    }
}