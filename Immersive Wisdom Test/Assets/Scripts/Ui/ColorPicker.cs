using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public enum RGB
    {
        RED,
        GREEN,
        BLUE
    }

    [Serializable]
    public struct ColorSlider
    {
        public RGB rgb;
        public ColorPickerGradient cpGrad;
        public Slider slider;
        public TMP_InputField text;
    }

    [Serializable]
    public struct OpacitySlider
    {
        public Slider slider;
        public TMP_InputField text;
    }

    public List<ColorSlider> colorSliders;
    public TMP_InputField hex;
    public OpacitySlider opacity;
    public ColorPanel colorPanel;

    public void Start()
    {
        UpdateSliders();
    }

    public void UpdateSliders()
    {
        foreach (ColorSlider colorSlider in colorSliders)
        {
            colorSlider.cpGrad.UpdateColors(
                new Color(colorSlider.rgb == RGB.RED ? 0 : colorSliders[0].slider.value, colorSlider.rgb == RGB.GREEN ? 0 : colorSliders[1].slider.value, colorSlider.rgb == RGB.BLUE ? 0 : colorSliders[2].slider.value, 1),
                new Color(colorSlider.rgb == RGB.RED ? 1 : colorSliders[0].slider.value, colorSlider.rgb == RGB.GREEN ? 1 : colorSliders[1].slider.value, colorSlider.rgb == RGB.BLUE ? 1 : colorSliders[2].slider.value, 1));
            colorSlider.text.text = "" + Mathf.Floor(colorSlider.slider.value * 255);
        }
        opacity.text.text = Mathf.Floor(opacity.slider.value * 100) + "%";

        Color color = new Color(colorSliders[0].slider.value, colorSliders[1].slider.value, colorSliders[2].slider.value, opacity.slider.value);
        hex.text = ColorUtility.ToHtmlStringRGB(color);
        colorPanel.Color = color;
    }

    public void UpdateHexCode()
    {
        Color color;
        if (hex.text.Length != 6 || !ColorUtility.TryParseHtmlString("#" + hex.text, out color))
        {
            UpdateSliders();
            return;
        }

        colorSliders[0].slider.value = color.r;
        colorSliders[1].slider.value = color.g;
        colorSliders[2].slider.value = color.b;
        UpdateSliders();
    }

    public void UpdateInputColor()
    {
        foreach (ColorSlider colorSlider in colorSliders)
        {
            colorSlider.slider.value = int.Parse(colorSlider.text.text) / 255f;
        }
        UpdateSliders();
    }

    public void UpdateOpacity()
    {
        int result;
        if (int.TryParse(opacity.text.text, out result))
        {
            opacity.slider.value = result / 100f;
        }
        UpdateSliders();
    }
}
