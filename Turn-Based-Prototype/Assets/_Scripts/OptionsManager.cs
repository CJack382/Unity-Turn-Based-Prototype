using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private bool muteAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
