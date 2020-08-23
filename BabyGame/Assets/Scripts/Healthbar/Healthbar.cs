using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void setMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
        setValue(maxValue);
    }

    public void setValue(float value)
    {
        slider.value = value;
    }
}
