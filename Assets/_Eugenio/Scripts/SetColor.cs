using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random=UnityEngine.Random;

public class SetColor : MonoBehaviour
{
    //set color sprite
    public static Action<Color> onButtonClick;
    private Color _newColor;
    public Image _currentImage;

    void OnEnable() {
        RandoColor();
    }

    public void RandoColor()
    {
        _newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(1f, 0f),1);
        _currentImage.color = _newColor;
    }

    public void OnPress()
    {
        if(onButtonClick!= null)
            onButtonClick(_newColor);
    }
}