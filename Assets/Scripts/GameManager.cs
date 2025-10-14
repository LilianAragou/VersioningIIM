using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int life = 3;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void GameOver(bool win)
    {
        if (win)
        {
            Debug.Log("You Win!");
        }
        else
        {
            Debug.Log("Game Over!");
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (life <= 0)
        {
            GameOver(false);
        }
    }
}
