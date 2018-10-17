using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text m_VolumeText;
    public float m_fVolumeLevel;

    public GameObject slider;

    private GameObject m_pFadecanvas;

    private void Awake()
    {
        m_fVolumeLevel = slider.GetComponent<Slider>().value;
        m_pFadecanvas = GameObject.Find("Fade Canvas");
    }


    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = slider.GetComponent<Slider>().value;

        m_fVolumeLevel = slider.GetComponent<Slider>().value;
        m_VolumeText.text = ((int)(slider.GetComponent<Slider>().value * 100.0f)).ToString();
    }

    public void NextScene()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Menu Select");

        m_pFadecanvas.GetComponent<LevelChanger>().FadeToLevel("Level 1");

        GameObject.Find("AudioManager").GetComponent<AudioManager>().Swap("Main Menu", "Level 1", 20.0f);



    }

    public void CloseApp()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Menu Select");
        Application.Quit();
    }
}
