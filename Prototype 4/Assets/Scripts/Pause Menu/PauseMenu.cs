using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public  bool GameIsPaused = false;
    public  bool FirstFrame = false;

    public GameObject PauseMenuUI;

    void Start()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        FirstFrame = false;
        PauseMenuUI.SetActive(true);
        
    }
	
	// Update is called once per frame
	void Update () {

        if(FirstFrame == false)
        {
            PauseMenuUI.SetActive(false);
            FirstFrame = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (GameIsPaused)
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
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;

        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;

        GameIsPaused = true;
    }

    public void MainMenu()
    {
        
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
