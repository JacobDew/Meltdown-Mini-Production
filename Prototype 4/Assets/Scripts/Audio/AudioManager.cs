using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public GameObject Slider;

    // Use this for initialization
    void Awake()
    {

        Slider = GameObject.Find("Slider");

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

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

    }

    public void Play(string name)
    {
      

        Sound s = Array.Find(sounds, sound => sound.Name == name);

        if (name == "Laser")
        {
            s.source.pitch = UnityEngine.Random.Range(0.4f, 1f);
            s.source.volume = 0.7f;

        }
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
            
        }
        s.source.Play();

    }
}
