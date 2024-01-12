using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverWheelControl : MonoBehaviour
{
    [Header("UI Elements")] 
    
    [Header("Wheel Controls")]
    public Sprite[] leverSprites;
    public Image leverSprite;
    public Slider slider;
    

    [HideInInspector]
    public event EventHandler<OnWheelChangeEventArgs> OnWheelChange;
    public class OnWheelChangeEventArgs : EventArgs
    {
        public int sliderValue;
    }

    public void ChangedValues()
    {
        ChangeWheelControls();
    }
    
    public void ChangeWheelControls()
    {
        OnWheelChange?.Invoke(this, new OnWheelChangeEventArgs{sliderValue = (int)slider.value});
        ChangeLeverSprite();
    }
    private void ChangeLeverSprite()
    {
        var leverValue = slider.value;
        
        switch (leverValue)
        {
            case -2:
                leverSprite.sprite = leverSprites[4];
                break;
            case -1:
                leverSprite.sprite = leverSprites[3];
                break;
            case 0:
                leverSprite.sprite = leverSprites[2];
                break;
            case 1:
                leverSprite.sprite = leverSprites[1];
                break;
            case 2:
                leverSprite.sprite = leverSprites[0];
                break;
            default:
                break;
        }
    }
    
    
}