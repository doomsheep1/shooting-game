using UnityEngine.Audio;
using System;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0.0001f, 1f)]
    public float volume;
    [Range(0.1f, 1f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
    public AudioMixerGroup mixerGroup;
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance; // singleton

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) // meaning no audiomanager in the scene
            instance = this; // assign audiomanager to the instance
        else
        {
            Destroy(gameObject); // if have already, meaning extra, destroy
            return; // to prevent any unnecessary calls of other methods
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    void Start()
    {
        // add condition to check which scene i am in then play the respective scene music
        PlayMusic("bgm");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            UnityEngine.Debug.Log("Unable to find the name of this sound");
            return;
        }
        s.source.Play();
    }
}
