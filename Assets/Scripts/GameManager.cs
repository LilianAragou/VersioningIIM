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
            SoundManager.Instance.PlayWin();
        }
        else
        {
            SoundManager.Instance.PlayLose();
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
        if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
        {
            GameOver(true);
        }
    }
}
