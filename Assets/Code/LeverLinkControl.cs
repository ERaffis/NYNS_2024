using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverLinkControl : MonoBehaviour
{
    
    [Header("Status")]
    public bool isLeverLinked;
    [Header("UI Elements")] 
    private Button _linkLeverButton;
    public Sprite leverLinkedSprite;
    public Sprite leverUnlinkedSprite;
    
    
    [Header("Links")]
    public Slider leftLever;
    public Slider rightLever;
    

    private LeverWheelControl leftWheelUI;
    private LeverWheelControl rightWheelUI;
   

    

    private void Start()
    {
        _linkLeverButton = this.gameObject.GetComponent<Button>();
        isLeverLinked = true;
        leftWheelUI = leftLever.gameObject.GetComponent<LeverWheelControl>();
        rightWheelUI = rightLever.gameObject.GetComponent<LeverWheelControl>();
        leftWheelUI.OnWheelChange += ChangeLeftInput;
        rightWheelUI.OnWheelChange += ChangeRightInput;
    }

    private void ChangeLeftInput(object sender, LeverWheelControl.OnWheelChangeEventArgs e)
    {
        if (isLeverLinked)
        {
            rightLever.value = leftLever.value;
        }
    }

    private void ChangeRightInput(object sender, LeverWheelControl.OnWheelChangeEventArgs e)
    {
        if (isLeverLinked)
        {
            leftLever.value = rightLever.value;
        }
    }

    public void LinkLevers()
    {
        isLeverLinked = !isLeverLinked;
        _linkLeverButton.image.sprite = isLeverLinked ? leverLinkedSprite : leverUnlinkedSprite;
    }

    public void LinkLevers(bool desiredState)
    {
        isLeverLinked = desiredState;
        _linkLeverButton.image.sprite = isLeverLinked ? leverLinkedSprite : leverUnlinkedSprite;
    }
    
}