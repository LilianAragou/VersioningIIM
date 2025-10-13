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

    void Start()
    {

    }

    void Update()
    {
        
    }
}
