using UnityEngine;
using System.Collections;

public class Pizza : MonoBehaviour
{
    private Vector2 direction;
    public float startSpeed;
    public float speed;
    public Transform spawnPoint;
    private bool hasHit = false;
    public GameObject pizzaObject;
    void Start()
    {
        ResetPosition();
    }
    void Update()
    {
        if (!hasHit)
        {
            transform.Translate(direction * startSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Block"))
        {
            Rebound(other);
            hasHit = true;
        }
        if (other.gameObject.CompareTag("Block"))
        {
            //AddIngredient(other);
            //HandleBonus(other);
            Destroy(other.gameObject);
            hasHit = true;
        }
        if (other.gameObject.CompareTag("Lose"))
        {
            GameManager.Instance.life -= 1;
            StartCoroutine(RespawnPizza(1f));
        }
    }
    void ResetPosition()
    {
        transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        direction = new Vector2(0, -1).normalized;
        hasHit = false;
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
    private IEnumerator RespawnPizza(float delay)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(delay);

        ResetPosition();

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

}
