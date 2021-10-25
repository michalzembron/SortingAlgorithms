using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] music;
    public Sound[] effects;

    public static AudioManager instance;
    public AudioMixerGroup audioMixerGroup;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);

        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = audioMixerGroup;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        foreach (Sound s in effects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = audioMixerGroup;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(music, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void PlayAudioEffect(string name)
    {
        Sound s = Array.Find(effects, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void ChangeBackgroundMusic()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            StartCoroutine(FadeMusic("SortingLevel", "MainMenu"));
        else if (SceneManager.GetActiveScene().buildIndex == 1)
            StartCoroutine(FadeMusic("MainMenu", "SortingLevel"));
        else
            Debug.LogWarning("Scene: " + SceneManager.GetActiveScene().buildIndex + " is not supported!");
    }

    private IEnumerator FadeMusic(string soundFrom, string soundTo)
    {
        Sound s = Array.Find(music, sound => sound.name == soundFrom);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + soundFrom + " not found!");
            yield break;
        }

        // Slowly mute current song
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            s.source.volume -= .01f;
            if (s.source.volume <= 0)
            {
                s.source.Stop();
                PlayMusic(soundTo);
                break;
            }
        }

        s = Array.Find(music, sound => sound.name == soundTo);
        s.source.volume = 0;

        // Slowly increase volume of next song
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            s.source.volume += .02f;
            if (s.source.volume >= .25f)
            {
                s.source.volume = .25f;
                break;
            }
        }
    }
}
