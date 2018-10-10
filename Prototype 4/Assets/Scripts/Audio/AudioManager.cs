using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    private GameObject Slider;

    public static AudioManager instance;

    // Use this for initialization
    void Awake()
    {

        if (instance == null )
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
        Play("BackgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Sound s in sounds)
        {
            if(Slider == null)
            {
                Slider = GameObject.Find("Pause Slider");
                Slider.GetComponent<Slider>().value = s.source.volume;

            }

            s.source.volume = Slider.GetComponent<Slider>().value;
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
}
