using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class cameraAdattata : MonoBehaviour
{
    float newAspectRatio = 16f / 9f;
    public CanvasScaler canvas;

    private void Awake()
    {
        Camera.main.rect = new Rect(0, 0, 1, 1);
    }

    void Start()
    {
        float variance = newAspectRatio / Camera.main.aspect;
        if (variance < newAspectRatio)
        {
            canvas.matchWidthOrHeight = 1;
        }
        if (variance < 1.0f)
            Camera.main.rect = new Rect((1.0f - variance) / 2.0f, 0, variance, 1.0f);
        else
        {
            variance = 1.0f / variance;
            Camera.main.rect = new Rect(0, (1.0f - variance) / 2.0f, 1.0f, variance);
        }
    }
}