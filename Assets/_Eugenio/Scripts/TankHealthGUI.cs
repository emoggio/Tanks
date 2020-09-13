using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealthGUI : MonoBehaviour
{
    [SerializeField]
    private Slider m_Slider;                             // The slider to represent how much health the tank currently has.

    [SerializeField]
    private Image m_FillImage;                           // The image component of the slider.
    [SerializeField]
    private Color m_FullHealthColor = Color.green;


    void Start()
    {
        SetColor( m_FullHealthColor);
    }

    public void SetColor(Color color)
    {
       m_FullHealthColor= color;
       m_FillImage.color = m_FullHealthColor;
    }

   public void UpdateHealth(float healht)
    {
        m_Slider.value = healht;
    }
}
