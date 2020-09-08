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
    private Image _currentImage;

    //setColorMaterial
    //public static Action<Material> onClick;

    void Start()
    {
        _currentImage = this.GetComponent<Image>();
        _newColor = _currentImage.color;
    }

    public void OnPress()
    {
        if(_currentImage==null)
            _newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        if(onButtonClick!= null)
            onButtonClick(_newColor);
    }
}