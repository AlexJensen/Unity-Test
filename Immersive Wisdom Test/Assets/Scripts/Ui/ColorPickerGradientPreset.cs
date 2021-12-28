using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerGradientPreset : ColorPickerGradient
{
    public Color leftColor, rightColor;
    protected override void Awake()
    {
        base.Awake();
        UpdateColors(leftColor, rightColor);
    }
}
