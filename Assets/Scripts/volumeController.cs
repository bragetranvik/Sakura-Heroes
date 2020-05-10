using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeController : MonoBehaviour
{
    public AudioSource thisAudioSource;
    public static float volume;
    public static bool startValueSet = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!startValueSet)
        {
            ChangeVolume(0.125f);
            startValueSet = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        thisAudioSource.volume = volume;
        ChangeVolume(volume);
    }

    public void ChangeVolume(float newVolume)
    {
        volume = newVolume;
        thisAudioSource.volume = volume;
    }
}
