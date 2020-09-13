using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random=UnityEngine.Random;
using DG.Tweening;

public class SetColor : MonoBehaviour
{
    public static Action<Color> onButtonClick;
    public static Action onButtonClickVFX;
    private Color _newColor;
    public Image CurrentImage;
    public GameObject ButtonBounce;

    private float _yPos;

    void OnEnable() {
        RandoColor();
    }

    public void RandoColor()
    {
        _newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(1f, 0f),1);
        CurrentImage.color = _newColor;
    }

    public void Bounce()
    {
        //RectTransform myRectTransform = ButtonBounce.GetComponent<RectTransform>();
       // _yPos= myRectTransform.anchoredPosition.y+200;

        ButtonBounce.transform.DOMoveY(200,.8f).SetEase(Ease.InBack).SetLoops(2, LoopType.Yoyo).OnComplete(OnPress);
    }

    public void OnPress()
    {
        if(onButtonClick!= null)
            onButtonClick(_newColor);

    }


}