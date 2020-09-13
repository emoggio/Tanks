using System.Collections;
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
        //public RectTransform SettingPos;
        private float _yPos;
        private Button _buttonSetting;
        private Image _imageSetting, _cogImage;
        private GameObject _cog;
        public RectTransform ButtonColor;
        public RectTransform ButtonXXX;
        public RectTransform ButtonYYY;

        //VFX
        public GameObject ExplosionVFX;
        private CanvasGroup _canvasGroupVFX;

        //scrollview
        public RectTransform SwatchesScroll;

        //animations
        private float _time = .6f;
        Color _grey = new Color(.4f,.4f,.4f,1);

        private void Awake() 
        {
            InstatiateButtons();
            _buttonSetting = Setting.GetComponent<Button>();
            _imageSetting = Setting.GetComponent<Image>();
            _cog = Setting.gameObject.transform.GetChild(0).gameObject;
            _cogImage = _cog.GetComponent<Image>();
            _canvasGroupVFX = ExplosionVFX.GetComponent<CanvasGroup>();

            Sequence resetVFX = DOTween.Sequence();
            resetVFX.Append(ExplosionVFX.transform.DOScaleX(0,0))
                .Join(ExplosionVFX.transform.DOScaleY(0,0));

            Invoke("RaycastSwitcher", 1.5f);
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
        }

        public void OnDisable()
        {
            SetColor.onButtonClick -= UpdateColor;
        }
        
        public void ShowButtonMenu()
        {
            RaycastSwitcher();
            
            Vector2 _ancorMin =  new Vector2(.5f,.26f);
            Vector2 _ancorMax =  new Vector2(.5f,.26f);

             Sequence Show = DOTween.Sequence();
             Show.Append(DOTween.To(()=> ButtonColor.anchorMax, x=> ButtonColor.anchorMax = x, _ancorMax,_time)).SetEase(Ease.OutBack)
                 .Join(DOTween.To(()=> ButtonColor.anchorMin, x=> ButtonColor.anchorMin = x, _ancorMin,_time)).SetEase(Ease.OutBack)
                 .Append(DOTween.To(()=> ButtonXXX.anchorMax, x=> ButtonXXX.anchorMax = x, _ancorMax,_time)).SetEase(Ease.OutBack)
                 .Join(DOTween.To(()=> ButtonXXX.anchorMin, x=> ButtonXXX.anchorMin = x, _ancorMin,_time)).SetEase(Ease.OutBack)
                 .Append(DOTween.To(()=> ButtonYYY.anchorMax, x=> ButtonYYY.anchorMax = x, _ancorMax,_time)).SetEase(Ease.OutBack)
                 .Join(DOTween.To(()=> ButtonYYY.anchorMin, x=> ButtonYYY.anchorMin = x, _ancorMin,_time)).SetEase(Ease.OutBack);
        }

        public void HideButtonMenu()
        {
            Vector2 _ancorMin =  new Vector2(.5f,0f);
            Vector2 _ancorMax =  new Vector2(.5f,0f);

            Sequence Hide = DOTween.Sequence();
            Hide.Append(DOTween.To(()=> ButtonColor.anchorMax, x=> ButtonColor.anchorMax = x, _ancorMax,_time)).SetEase(Ease.OutBack)
                .Join(DOTween.To(()=> ButtonColor.anchorMin, x=> ButtonColor.anchorMin = x, _ancorMin,_time)).SetEase(Ease.OutBack)
                .Append(DOTween.To(()=> ButtonXXX.anchorMax, x=> ButtonXXX.anchorMax = x, _ancorMax,_time)).SetEase(Ease.OutBack)
                .Join(DOTween.To(()=> ButtonXXX.anchorMin, x=> ButtonXXX.anchorMin = x, _ancorMin,_time)).SetEase(Ease.OutBack)
                .Append(DOTween.To(()=> ButtonYYY.anchorMax, x=> ButtonYYY.anchorMax = x, _ancorMax,_time)).SetEase(Ease.OutBack)
                .Join(DOTween.To(()=> ButtonYYY.anchorMin, x=> ButtonYYY.anchorMin = x, _ancorMin,_time)).SetEase(Ease.OutBack).OnComplete(ShowSwatches);
        }

        public void ShowSwatches()
        {
            Vector2 _ancorMinScroll =  new Vector2(0f,0.3f);
            Vector2 _ancorMaxScroll =  new Vector2(1f,0.3f);

            Sequence ShowSwatch = DOTween.Sequence();
            ShowSwatch.Append(DOTween.To(()=> SwatchesScroll.anchorMax, x=> SwatchesScroll.anchorMax = x, _ancorMaxScroll,_time)).SetEase(Ease.OutBack)
                .Join(DOTween.To(()=> SwatchesScroll.anchorMin, x=> SwatchesScroll.anchorMin = x, _ancorMinScroll,_time)).SetEase(Ease.OutBack);
        }

        public void HideSwatches()
        {
            Vector2 _ancorMinScroll =  new Vector2(0f,0f);
            Vector2 _ancorMaxScroll =  new Vector2(1f,0f);

            Sequence HideSwatch = DOTween.Sequence();
            HideSwatch.Append(DOTween.To(()=> SwatchesScroll.anchorMax, x=> SwatchesScroll.anchorMax = x, new Vector2(1, 0),_time)).SetEase(Ease.OutBack)
                .Join(DOTween.To(()=> SwatchesScroll.anchorMin, x=> SwatchesScroll.anchorMin = x, new Vector2(0, 0),_time)).SetEase(Ease.OutBack).OnComplete(RaycastSwitcher);
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
                _tButton.transform.localScale = new Vector3(1,1,1);
                SetColorScript.RandoColor();
            }
        }
    }

}
