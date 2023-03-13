using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixekArtDrawingUI : MonoBehaviour
{
    [SerializeField] private Texture2D colorsTexture;

    private Image selectedColorImage;

    private void Start()
    {
        transform.Find("SmallButton").GetComponent<Button>().onClick.AddListener(() => { PixelArtDrawSystem.Instance.SetCursorSize(PixelArtDrawSystem.CursorSize.Small); });
        transform.Find("MediumButton").GetComponent<Button>().onClick.AddListener(() => { PixelArtDrawSystem.Instance.SetCursorSize(PixelArtDrawSystem.CursorSize.Medium); });
        transform.Find("LargeButton").GetComponent<Button>().onClick.AddListener(() => { PixelArtDrawSystem.Instance.SetCursorSize(PixelArtDrawSystem.CursorSize.Large); });

        selectedColorImage = transform.Find("SelectedColor").GetComponent<Image>();

        PixelArtDrawSystem.Instance.OnColorChanged += PixelArtDraw_OnColorChanged;
    }

    private void PixelArtDraw_OnColorChanged(object sender, EventArgs e)
    {
        UpdateSelectedColor();
    }

    private void UpdateSelectedColor()
    {
        Vector2 pixelCoordinates = PixelArtDrawSystem.Instance.GetColorUV();
        pixelCoordinates.x *= colorsTexture.width;
        pixelCoordinates.y *= colorsTexture.height;
        selectedColorImage.color = colorsTexture.GetPixel((int)pixelCoordinates.x, (int)pixelCoordinates.y);
    }
}
