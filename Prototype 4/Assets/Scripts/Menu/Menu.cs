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

    private void Awake()
    {
        m_fVolumeLevel = slider.GetComponent<Slider>().value;
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
        SceneManager.LoadScene("Test");
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
