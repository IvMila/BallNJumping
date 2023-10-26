using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackgroundColor : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private List<Color32> ColorList = new List<Color32>();

    private void Awake()
    {
        var RandomIndexColor = Random.Range(0, ColorList.Count);
        _camera = GetComponent<Camera>();
        _camera.clearFlags = CameraClearFlags.SolidColor;

        Color32 NewColorBackground = ColorList[RandomIndexColor];
        _camera.backgroundColor = NewColorBackground;
    }
}
