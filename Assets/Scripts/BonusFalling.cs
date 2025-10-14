using UnityEngine;

public class BonusFalling : MonoBehaviour
{
    public float fallSpeed = 3f;
    public float destroyY = -10f;
    private Vector2 direction = Vector2.down;
    void Update()
    {
        transform.Translate(direction * fallSpeed * Time.deltaTime);
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}
