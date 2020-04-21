using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeController : MonoBehaviour
{
    public AudioSource thisAudioSource;
    public static float volume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        thisAudioSource.volume = volume;
    }

    public void ChangeVolume(float newVolume)
    {
        volume = newVolume;
        thisAudioSource.volume = volume;
    }
}
