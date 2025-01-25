using UnityEngine;

public class AudioObject : MonoBehaviour
{
    private static AudioObject instance;

    private void Awake()
    {
        // If an instance of the AudioObject already exists, destroy this one
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Otherwise, set this as the instance and make sure it doesn't get destroyed on load
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Optionally, ensure the music is playing when the scene starts
    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.Play(); // or use your preferred settings
        }
    }
}
