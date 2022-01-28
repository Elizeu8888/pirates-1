using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;



    public void SetMaxXP(float xp)
    {
        slider.maxValue = xp;
        slider.value = xp;

        fill.color = gradient.Evaluate(1f);

    }

    public void SetXP(float xp)
    {


        slider.value = xp;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

}
