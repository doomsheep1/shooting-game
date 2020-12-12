using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenuUI : MonoBehaviour
{
    public AudioMixer mixer;
    private AudioManager vol;

    void Awake()
    {
        vol = GameObject.FindGameObjectWithTag("volume").GetComponent<AudioManager>();
    }

    public void SetRes(int resIndex)
    {
        switch (resIndex)
        {
            case 0:
                Screen.SetResolution(1024, 768, false);
                UnityEngine.Debug.Log("Changed 0");
                break;
            case 1:
                Screen.SetResolution(1280, 960, false);
                UnityEngine.Debug.Log("Changed 1");
                break;
            default:
                Screen.SetResolution(1600, 900, false);
                UnityEngine.Debug.Log("Changed def");
                break;
        }
    }

    public void SetQuality(int qualIndex)
    {
        QualitySettings.SetQualityLevel(qualIndex);
        int qualityLevel = QualitySettings.GetQualityLevel();
        UnityEngine.Debug.Log(qualityLevel);
    }

    public void SetVolume(float sliderLevel)
    {
        //Sound s = Array.Find(vol.sounds, sound => sound.name == "bgm"); 
        /* lambda expression, sound is like a temp variable that references 
        an object in the vol.sounds array here that stores the matched "bgm" into s*/
        foreach (Sound s in vol.sounds)
        {
            s.volume = sliderLevel;
            s.source.volume = s.volume;
        }
        mixer.SetFloat("Vol", Mathf.Log10(sliderLevel) * 20);
    }
}
