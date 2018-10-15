using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    bool Isfading = false;

    public Sound[] sounds;

    private GameObject Slider;

    public static AudioManager instance;

    // Use this for initialization
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Slider = GameObject.Find("Pause Slider");


        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            Slider.GetComponent<Slider>().value = s.source.volume;

            s.source.volume = Slider.GetComponent<Slider>().value;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            Play("Main Menu");
        }
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            Play("Level 1");
        }
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            Play("Level 2");
        }
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            Play("Boss Level");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!Isfading)
        {
            foreach (Sound s in sounds)
            {
                if (Slider == null)
                {
                    Slider = GameObject.Find("Pause Slider");
                    Slider.GetComponent<Slider>().value = s.source.volume;

                }

                s.source.volume = Slider.GetComponent<Slider>().value;
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;

        }

        s.source.Play();

    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;

        }

        s.source.Stop();

    }

    public  void Swap(string From, string To, float FadeTime)
    {


        Sound sds = Array.Find(sounds, sound => sound.Name == From);


        if (sds == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
             return ; 

        }
        float startVolume = AudioListener.volume;

        Isfading = true;
      while (AudioListener.volume > 0)
        {
            Debug.Log("Volume: " + (sds.source.volume));
            Debug.Log("Elasped Time: " + ( startVolume * Time.time / FadeTime));

            AudioListener.volume -= (startVolume * Time.deltaTime / FadeTime);

        }

        sds.source.Stop();
        AudioListener.volume = startVolume;

       Isfading = false;

        Sound ss = Array.Find(sounds, sound => sound.Name == To);

        if (ss == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
             return; 

        }

        ss.source.Play();

    }
}
