using UnityEngine;
using UnityEngine.SceneManagement;

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
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Win")
        {
            SoundManager.Instance.PlayWin();
        }
        else if (scene.name == "Death")
        {
            SoundManager.Instance.PlayLose();
        }
    }
    public void GameOver(bool win)
    {
        if (win)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("Death");
        }
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
