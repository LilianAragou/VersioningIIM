using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float MaxGameTime = 30f;
    private float TimerValue;
    public static TimerManager Instance;
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
    public void UpdateTimerDisplay(float RemainingTime)
    {
        slider.value = RemainingTime / MaxGameTime;
    }
    void Start()
    {
        TimerValue = MaxGameTime;
    }
    void Update()
    {
        TimerValue -= Time.deltaTime;
        UpdateTimerDisplay(TimerValue);
        if (TimerValue <= 0f)
        {
            GameManager.Instance.life -= 1;
            TimerValue = MaxGameTime;
        }
    }
    public void AddTimer(float timeToAdd)
    {
        TimerValue += timeToAdd;
        if (TimerValue > MaxGameTime)
        {
            TimerValue = MaxGameTime;
        }
        UpdateTimerDisplay(TimerValue);
    }
}
