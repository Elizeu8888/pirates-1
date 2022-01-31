using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour
{
    public Slider slider;
    public Sound soundscript;
    public AudioSource audioSource;
    public GameObject audioman;


    void Start()
    {
        if (PlayerPrefs.HasKey("volume") == true)
        {
            slider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            slider.value = 0.5f;
        }
    }

    void Update()
    {
        SetVolume(slider.value);
        SetMaxVolume(1);
        PlayerPrefs.SetFloat("volume", slider.value);
    }

    public void SetMaxVolume(int volume)
    {
        slider.maxValue = volume;
        //slider.value = volume;
    }

    public void SetVolume(float volume)
    {
         audioman.GetComponent<AudioSource>().volume = slider.value;
    }
}
