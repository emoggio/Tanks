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
        private Image TankImageFG;

        //buttons
        public Button ButtonSetting;
        public GameObject ButtonColor;
        public GameObject ButtonPause;
        public GameObject ButtonQuit;

        //scrollview
        public GameObject SwatchesScroll;

        private float time = .6f;

        private CanvasGroup SwatchesCanvas;

        private void Awake() 
        {
            InstatiateButtons();
            SwatchesCanvas = SwatchesScroll.GetComponent<CanvasGroup>();
        }

        public void OnEnable() 
        {
            SetColor.onButtonClick += UpdateColor;
        }

        public void UpdateColor(Color newColor)
        {
            GameManagerScript.m_Tanks[0].m_TankIconFG.transform.DOShakeRotation(.8f,30f,0,30,true);
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
            ButtonSetting.interactable = false;

            Sequence HideShow = DOTween.Sequence();
            HideShow.Append(ButtonColor.transform.DOMoveY(115, time)).SetEase(Ease.OutBack)
                .Append(ButtonPause.transform.DOMoveY(115, time)).SetEase(Ease.OutBack)
                .Append(ButtonQuit.transform.DOMoveY(115, time)).SetEase(Ease.OutBack);
        }

        public void ShowSwatches()
        {
            Sequence HideShow = DOTween.Sequence();
            HideShow.Append(ButtonColor.transform.DOMoveY(-300, time/2)).SetEase(Ease.InBack)
                .Append(ButtonPause.transform.DOMoveY(-300, time/2)).SetEase(Ease.InBack)
                .Append(ButtonQuit.transform.DOMoveY(-300, time/2)).SetEase(Ease.InBack)
                .Append(SwatchesScroll.transform.DOMoveY(20, time)).SetEase(Ease.InBack);
        }

        public void HideSwatches()
        {
            SwatchesScroll.transform.DOMoveY(-500, time).SetEase(Ease.InBack).OnComplete(RaycastSwitcher);
        }

        void RaycastSwitcher()
        {
            ButtonSetting.interactable = true;
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
