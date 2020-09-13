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
    public RectTransform ButtonBounce;

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
        Vector2 _bounceYMin =  new Vector2(0f,0.5f);
        Vector2 _bounceYMax =  new Vector2(1f,1.5f);
        Vector2 _bounceYMinEnd =  new Vector2(0f,0f);
        Vector2 _bounceYMaxEnd =  new Vector2(1f,1f);
        float _time = .8f;

        Sequence BouncePos = DOTween.Sequence();
        BouncePos.Append(DOTween.To(()=> ButtonBounce.anchorMax, x=> ButtonBounce.anchorMax = x, _bounceYMax,_time)).SetEase(Ease.InBack)
                .Join(DOTween.To(()=> ButtonBounce.anchorMin, x=> ButtonBounce.anchorMin = x, _bounceYMin,_time)).SetEase(Ease.InBack)
                .Append(DOTween.To(()=> ButtonBounce.anchorMax, x=> ButtonBounce.anchorMax = x, _bounceYMaxEnd,_time)).SetEase(Ease.InBack)
                .Join(DOTween.To(()=> ButtonBounce.anchorMin, x=> ButtonBounce.anchorMin = x, _bounceYMinEnd,_time)).SetEase(Ease.InBack).OnComplete(OnPress);

        //ButtonBounce.transform.DOMoveY(200,.8f).SetEase(Ease.InBack).SetLoops(2, LoopType.Yoyo).OnComplete(OnPress);
    }

    public void OnPress()
    {
        if(onButtonClick!= null)
            onButtonClick(_newColor);

    }


}