using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Complete
{
    public class HUDManager : MonoBehaviour
    {
        //public Image TankIcon;
        //public TextMeshProUGUI PlayerTx;
        public GameManager GameManagerScript;

        [Range(1,10)]
        public int TankButtonAmount;
        public GameObject TankButton;
        public SetColor SetColorScript;
        public GameObject Parent;
        private Image TankImageFG;


        private void Awake() 
        {
            InstatiateButtons();
        }

        public void OnEnable() 
        {
            SetColor.onButtonClick += UpdateColor;
        }

        public void UpdateColor(Color newColor)
        {
            GameManagerScript.m_Tanks[0].m_TankIconFG.color = newColor;
            GameManagerScript.m_Tanks[0].m_PlayerTx.color = newColor;
        }

        public void OnDisable()
        {
            SetColor.onButtonClick -= UpdateColor;
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
