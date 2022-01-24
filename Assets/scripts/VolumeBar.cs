using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour
{
    public Slider slider;
    public Sound soundscript;

    public void SetMaxVolume(int volume)
    {
        slider.maxValue = volume;
        slider.value = volume;
    }

    public void SetVolume(int volume)
    {
        slider.value = soundscript.volume;
    }
}
