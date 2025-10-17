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
    public SpriteRenderer[] children;
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
            TimerManager.Instance.AddTimer(5f);
            Destroy(other.gameObject);
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
        if (other.gameObject.CompareTag("Player"))
        {
            float hitX = (transform.position.x - other.transform.position.x) / (other.collider.bounds.size.x / 2f);
            float angle = hitX * 60f * Mathf.Deg2Rad;
            direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)).normalized;
        }
        else
        {
            direction = Vector2.Reflect(direction, normal).normalized;
        }
    }


    private IEnumerator RespawnPizza(float delay)
    {
        Collider2D col = GetComponent<Collider2D>();

        SetRenderersActive(false);
        if (col != null) col.enabled = false;

        yield return new WaitForSeconds(delay);

        ResetPosition();

        SetRenderersActive(true);
        if (col != null) col.enabled = true;
    }

    void SetRenderersActive(bool active)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = active;

        children = GetComponentsInChildren<SpriteRenderer>();

        foreach (var child in children)
        {
            if (child != sr)
                child.enabled = active;
        }
    }
}
