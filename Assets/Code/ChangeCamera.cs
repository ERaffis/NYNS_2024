using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ChangeCamera : MonoBehaviour
{
    public CinemachineVirtualCamera firstPerson;
    public CinemachineVirtualCamera thirdPerson;

    public Button firstPersonButton;
    public Button thirdPersonButton;    
    
    public void SetFirstPerson()
    {
        firstPerson.Priority = 10;
        thirdPerson.Priority = 0;
        firstPersonButton.interactable = false;
        thirdPersonButton.interactable = true;
    }
    public void SetThirdPerson()
    {
        firstPerson.Priority = 0;
        thirdPerson.Priority = 10;
        firstPersonButton.interactable = true;
        thirdPersonButton.interactable = false;
    }
}
