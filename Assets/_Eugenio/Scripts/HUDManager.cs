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

    [Range(1,10)]
    public int TankButtonAmount;
    public GameObject TankButton;
    public GameObject Parent;
    private Image TankImageFG;

    private void Awake() 
    {
        InstatiateButtons();
    }

    private void InstatiateButtons()
    {
        for (int i = 0; i < TankButtonAmount; i++)
        {
            GameObject _tButton = Instantiate(TankButton, Parent.transform, true);
        }

    }

    public Component[] hingeJoints;

    void Start()
    {
        hingeJoints = GetComponentsInChildren<HingeJoint>();

        foreach (HingeJoint joint in hingeJoints)
            joint.useSpring = false;
    }

}
