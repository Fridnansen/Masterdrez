using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    public Button toggleButton;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    private bool isPlaying = true;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Sounds/background_music1");
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.Play();
    }

    public void ToggleMusic()
    {
        if (isPlaying)
        {
            audioSource.Pause();
            isPlaying = false;
        }
        else
        {
            audioSource.Play();
            isPlaying = true;
        }

        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (toggleButton != null)
        {
            Image icon = toggleButton.GetComponent<Image>();
            icon.sprite = isPlaying ? soundOnIcon : soundOffIcon;
        }
    }

    void Start()
    {
        UpdateButtonIcon();

        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleMusic);
        }
    }
}
