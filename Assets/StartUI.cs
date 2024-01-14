using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{

    public GameObject controlUI;
    public GameObject startUI;
    
    public void OpenControls()
    {
        startUI.SetActive(false);
        controlUI.SetActive(true);
    }

    public void CloseControls()
    {
        startUI.SetActive(true);
        controlUI.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
