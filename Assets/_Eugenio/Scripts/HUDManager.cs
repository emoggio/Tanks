﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Complete
{
    public class HUDManager : MonoBehaviour
    {
        public GameManager GameManagerScript;

        [Range(1,10)]
        public int TankButtonAmount;
        public GameObject TankButton;
        public SetColor SetColorScript;
        public GameObject Parent;

        //buttons
        public GameObject Setting;
        private Button _buttonSetting;
        private Image _imageSetting, _cogImage;
        private GameObject _cog;
        public GameObject ButtonColor;
        public GameObject ButtonXXX;
        public GameObject ButtonYYY;

        //VFX
        public GameObject ExplosionVFX;
        private CanvasGroup _canvasGroupVFX;


        //scrollview
        public GameObject SwatchesScroll;
        private CanvasGroup _swatchesCanvases;

        //animations
        private float _time = .6f;
        Color _grey = new Color(.4f,.4f,.4f,1);

        private void Awake() 
        {
            InstatiateButtons();
            _swatchesCanvases = SwatchesScroll.GetComponent<CanvasGroup>();
            _buttonSetting = Setting.GetComponent<Button>();
            _imageSetting = Setting.GetComponent<Image>();
            _cog = Setting.gameObject.transform.GetChild(0).gameObject;
            _cogImage = _cog.GetComponent<Image>();
            _canvasGroupVFX = ExplosionVFX.GetComponent<CanvasGroup>();

            Sequence resetVFX = DOTween.Sequence();
            resetVFX.Append(ExplosionVFX.transform.DOScaleX(0,0))
                .Join(ExplosionVFX.transform.DOScaleY(0,0));
        }

        public void OnEnable() 
        {
            SetColor.onButtonClick += UpdateColor;
        }

        public void UpdateColor(Color newColor)
        {
            Explosion();

            GameManagerScript.m_Tanks[0].m_TankIconFG.DOColor(newColor, .5f);
            GameManagerScript.m_Tanks[0].m_PlayerTx.DOColor(newColor, .5f);
            HideSwatches();
            Debug.Log(SwatchesScroll.GetComponent<CanvasGroup>().blocksRaycasts);
            SwatchesScroll.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDisable()
        {
            SetColor.onButtonClick -= UpdateColor;
        }
        
        public void ShowButtonMenu()
        {
            RaycastSwitcher();

            Sequence HideShow = DOTween.Sequence();
            HideShow.Append(ButtonColor.transform.DOMoveY(115, _time)).SetEase(Ease.OutBack)
                .Append(ButtonXXX.transform.DOMoveY(115, _time)).SetEase(Ease.OutBack)
                .Append(ButtonYYY.transform.DOMoveY(115, _time)).SetEase(Ease.OutBack);
        }

        public void ShowSwatches()
        {
            Sequence HideShow = DOTween.Sequence();
            HideShow.Append(ButtonColor.transform.DOMoveY(-300, _time/2)).SetEase(Ease.InBack)
                .Append(ButtonXXX.transform.DOMoveY(-300, _time/2)).SetEase(Ease.InBack)
                .Append(ButtonYYY.transform.DOMoveY(-300, _time/2)).SetEase(Ease.InBack)
                .Append(SwatchesScroll.transform.DOMoveY(20, _time)).SetEase(Ease.InBack);
        }

        public void HideSwatches()
        {
            SwatchesScroll.transform.DOMoveY(-500, _time).SetEase(Ease.InBack).OnComplete(RaycastSwitcher);
        }
        
        void RaycastSwitcher()
        {
            if(_buttonSetting.interactable == false)
            {
                _buttonSetting.interactable = true;
                _imageSetting.color = Color.white;
                _cogImage.color = Color.white;
            }
            else if(_buttonSetting.interactable == true)
            {
                _buttonSetting.interactable = false;
                _imageSetting.color = _grey;
                _cogImage.color = _grey;
            }
        }

        void Explosion()
        {
            Sequence VFX = DOTween.Sequence();
            VFX.Append(ExplosionVFX.transform.DOScaleX(1.5f,_time)).SetEase(Ease.OutCubic)
                .Join(ExplosionVFX.transform.DOScaleY(1.5f,_time)).SetEase(Ease.OutCubic)
                .Join(DOTween.To(()=> _canvasGroupVFX.alpha, x=> _canvasGroupVFX.alpha = x, 0f, _time*2)).SetEase(Ease.InSine)
                .AppendInterval(.2f)
                .Append(ExplosionVFX.transform.DOScaleX(0f,0))
                .Join(ExplosionVFX.transform.DOScaleY(0f,0))
                .Join(DOTween.To(()=> _canvasGroupVFX.alpha, x=> _canvasGroupVFX.alpha = x, 1f,0));
        }

        private void InstatiateButtons()
        {
            for (int i = 0; i < TankButtonAmount; i++)
            {
                GameObject _tButton = Instantiate(TankButton, Parent.transform, true);
                SetColorScript.RandoColor();
            }
        }
    }

}
