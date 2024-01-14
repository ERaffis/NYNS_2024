using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public GameObject winUI;
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void KeepPlaying()
    {
        winUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
