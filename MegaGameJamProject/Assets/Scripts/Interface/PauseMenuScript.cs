using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUi;


    private void Start()
    {

        pauseMenuUi.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

}