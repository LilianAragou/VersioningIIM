using UnityEngine;

public class Movement : MonoBehaviour
{
    public float acceleration = 25f;
    public float deceleration = 15f;
    public float maxSpeed = 13f;
    public float minSpeed = 3f;
    private float currentSpeed = 0f;
    private float moveDirection = 0f;
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1f;
        }
        else
        {
            moveDirection = 0f;
        }
        if (moveDirection != 0)
        {
            currentSpeed = Mathf.Max(currentSpeed, minSpeed);
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0f);
        }
        transform.Translate(Vector2.right * moveDirection * currentSpeed * Time.deltaTime);
    }
}
