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
}
