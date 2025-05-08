using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource musicSource;
    [SerializeField] private AudioClip  LevelComplete;


    private void Awake()
    { 
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

   
    public void PlayMusic()
    {
        musicSource.clip = LevelComplete;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}

