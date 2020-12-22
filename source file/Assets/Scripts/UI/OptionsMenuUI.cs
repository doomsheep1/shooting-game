using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioManager vol;
    public Slider volumeSlider;
    public TMPro.TMP_Dropdown qualDropdown;
    public TMPro.TMP_Dropdown rezDropdown;
    private int res1, res2;

    void Start()
    {
        LoadRes();
        LoadQual();
        LoadVol();
    }

    private void LoadRes ()
    {
        if (PlayerPrefs.HasKey("res1") & PlayerPrefs.HasKey("res2"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("res1"), PlayerPrefs.GetInt("res2"), false);
            switch (PlayerPrefs.GetInt("res1"))
            {
                case 1024:
                    rezDropdown.value = 0;
                    break;
                case 1280:
                    rezDropdown.value = 1;
                    break;
                default:
                    rezDropdown.value = 2;
                    break;
            }
        }
        else
        {
            UnityEngine.Debug.Log("No key found for resolution");
        }
    }

    private void LoadQual ()
    {
        if (PlayerPrefs.HasKey("qual"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qual"));
            qualDropdown.value = PlayerPrefs.GetInt("qual");
        }
        else
        {
            UnityEngine.Debug.Log("No key found for quality");
        }
    }

    private void LoadVol ()
    {
        if (PlayerPrefs.HasKey("vol"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("vol");
            foreach (Sound s in vol.sounds)
            {
                s.volume = PlayerPrefs.GetFloat("vol");
                s.source.volume = s.volume;
            }
            mixer.SetFloat("Vol", Mathf.Log10(PlayerPrefs.GetFloat("vol")) * 20);
        }
        else
        {
            UnityEngine.Debug.Log("No key found for volume");
        }
    }

    public void SetRes(int resIndex)
    {
        switch (resIndex)
        {
            case 0:
                Screen.SetResolution(1024, 768, false);
                res1 = 1024;
                res2 = 768;
                UnityEngine.Debug.Log("Changed 0");
                break;
            case 1:
                Screen.SetResolution(1280, 960, false);
                res1 = 1280;
                res2 = 960;
                UnityEngine.Debug.Log("Changed 1");
                break;
            default:
                Screen.SetResolution(1600, 900, false);
                res1 = 1600;
                res2 = 900;
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

    public void SaveSettings ()
    {
        PlayerPrefs.SetInt("res1", res1);
        PlayerPrefs.SetInt("res2", res2);
        PlayerPrefs.SetInt("qual", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetFloat("vol", volumeSlider.value);
    }
}
