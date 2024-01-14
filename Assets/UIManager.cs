using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startUi;
    public GameObject pauseUi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !startUi.activeSelf)
        {
            if (pauseUi.activeSelf)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseUi.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pauseUi.SetActive(false);
    }
}
