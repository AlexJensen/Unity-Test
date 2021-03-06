using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.UI;

public class ColorWheel : MonoBehaviour
{
    [Serializable]
    public struct OpacitySlider
    {
        public Slider slider;
        public TMP_InputField input;
    }

    [SerializeField]
    ColorWheelSelector wheelSelector;
    [SerializeField]
    Slider brightnessSlider;
    [SerializeField]
    OpacitySlider opacity;
    [SerializeField]
    ColorPanel colorPanel;

    Color currentColor = Color.white;

    public void UpdateSliders()
    {
        wheelSelector.UpdateWheelColor(new Color(brightnessSlider.value, brightnessSlider.value, brightnessSlider.value, opacity.slider.value));
        opacity.input.text = Mathf.Floor(opacity.slider.value * 100) + "%";
        UpdateBackgroundColor(currentColor);
    }

    public void UpdateBackgroundColor(Color color)
    {
        currentColor = color;
        color *= brightnessSlider.value;
        color[3] = opacity.slider.value;
        colorPanel.Color = color;
    }

    public void UpdateOpacity()
    {
        int result;
        if (int.TryParse(opacity.input.text, out result))
        {
            opacity.slider.value = result / 100f;
        }
        UpdateSliders();
    }
}
