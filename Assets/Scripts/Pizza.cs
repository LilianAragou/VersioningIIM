using UnityEngine;
using System.Collections;

public class Pizza : MonoBehaviour
{
    private Vector2 direction;
    public float startSpeed;
    public float speed;
    public float bonusSpeed;
    public Transform spawnPoint;
    private bool hasHit = false;
    public GameObject pizzaObject;
    public float transperceTimer = 0f;
    public float speedTimer = 0f;
    public float spriteTimer = 0f;
    private float spriteTimerInterval = 0.5f;
    private float spriteTimerElapsed = 0f;
    public SpriteRenderer sprite;
    public SpriteRenderer[] children;
    void Start()
    {
        ResetPosition();
        sprite = GetComponent<SpriteRenderer>();
        children = GetComponentsInChildren<SpriteRenderer>();
    }
    void Update()
    {
        if (!hasHit)
        {
            transform.Translate(direction * startSpeed * Time.deltaTime);
        }
        else
        {
            if (speedTimer > 0)
            {
                transform.Translate(direction * (speed + bonusSpeed) * Time.deltaTime);
            }
            else
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        if (transperceTimer > 0)
        {
            transperceTimer -= Time.deltaTime;
        }
        if (speedTimer > 0)
        {
            speedTimer -= Time.deltaTime;
        }
        if (spriteTimer > 0)
        {
            spriteTimer -= Time.deltaTime;
            spriteTimerElapsed += Time.deltaTime;
            if (sprite != null && spriteTimerElapsed >= spriteTimerInterval)
            {
                Debug.Log("TOGGLE");
                sprite.enabled = !sprite.enabled;
                children = GetComponentsInChildren<SpriteRenderer>();
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.enabled = sprite.enabled;
                    }
                }
                spriteTimerElapsed = 0f;
            }
        }
        else
        {
            if (sprite != null && !sprite.enabled)
            {
                sprite.enabled = true;
                children = GetComponentsInChildren<SpriteRenderer>();
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.enabled = sprite.enabled;
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Block"))
        {
            if (!(transperceTimer > 0 && other.gameObject.CompareTag("Block")))
            {
                Rebound(other);
            }
            Debug.Log("Timer " + other.gameObject.name);
            hasHit = true;
        }

        if (other.gameObject.CompareTag("Block"))
        {
            Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
            if (ingredient != null)
            {
                AddIngredient(ingredient.ingredientName);
                ingredient.HandleBonus();
            }
            //other.GetComponent<BlockHP>().TakeDamage(1);
            hasHit = true;
        }

        if (other.gameObject.CompareTag("Lose"))
        {
            GameManager.Instance.life -= 1;
            StartCoroutine(RespawnPizza(1f));
        }
    }
    void AddIngredient(string ingredientName)
    {
        Transform child = transform.Find(ingredientName);

        if (child != null)
        {
            child.gameObject.SetActive(true);
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
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Collider2D col = GetComponent<Collider2D>();

        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        yield return new WaitForSeconds(delay);

        ResetPosition();

        if (sr != null) sr.enabled = true;
        if (col != null) col.enabled = true;
    }
}
