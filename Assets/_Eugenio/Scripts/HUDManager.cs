using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Image TankIcon;

    public void OnEnable() 
    {
        SetColor.onButtonClick += UpdateColor;
    }

    public void UpdateColor(Color newColor)
    {
        TankIcon.color = newColor;
    }

    public void OnDisable()
    {
        SetColor.onButtonClick -= UpdateColor;
    }

    public int TankButtonAmount;
    public GameObject TankButton;
    public GameObject Parent;

    private void Awake() 
    {
        InstatiateButtons();
    }

    private void InstatiateButtons()
    {
        //streaksPool=new SteakBarElement[streakAmount.Lenght];
        for (int i = 0; i < TankButtonAmount; i++)
        {
            GameObject _tButton = Instantiate(TankButton, Parent.transform, true);
            //TankButton.transform.parent = Parent.transform;
            //TankButtonPools[i]=_tButton.AddComponent<SteakBarElement>();
        }
    }

}
