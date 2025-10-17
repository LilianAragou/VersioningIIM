using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource sfxSource;

    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip bonusClip;
    public AudioClip malusClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayWin() => PlaySFX(winClip);
    public void PlayLose() => PlaySFX(loseClip);
    public void PlayMalus() => PlaySFX(malusClip);
    public void PlayBonus() => PlaySFX(bonusClip);
}
