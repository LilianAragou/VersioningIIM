using UnityEngine;

public class Pizza : MonoBehaviour
{
    private Vector2 direction;
    public float speed;
    public Transform spawnPoint;
    void Start()
    {
        ResetPosition();
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Block"))
        {
            Rebound(other);
        }
        if (other.gameObject.CompareTag("Block"))
        {
            //AddIngredient(other);
            //HandleBonus(other);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Lose"))
        {
            GameManager.Instance.life -= 1;
            ResetPosition();
        }
    }
    void ResetPosition()
    {
        transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        direction = new Vector2(0.1f, 1).normalized;
    }
    void Rebound(Collision2D other)
    {
        Vector2 normal = other.contacts[0].normal;
        direction = Vector2.Reflect(direction, normal).normalized;
        if (other.gameObject.CompareTag("Player"))
        {
            direction.x += 0.5f * (transform.position.x - other.gameObject.GetComponent<Transform>().position.x);
        }
    }
}
