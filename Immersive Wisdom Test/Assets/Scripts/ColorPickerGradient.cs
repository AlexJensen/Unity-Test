using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerGradient : MonoBehaviour
{
    Image image;
    Material material;

    private void Awake()
    {
        image = GetComponent<Image>();
        Material mat = Instantiate(image.material);
        image.material = mat;
    }

    public void UpdateColors(Color leftColor, Color rightColor)
    {
        Material mat = image.material;
        mat.SetColor("_Color1", leftColor);
        mat.SetColor("_Color2", rightColor);

        // https://forum.unity.com/threads/masked-ui-elements-shader-not-updating.371542/
        image.enabled = false;
        image.enabled = true;
    }
}
