using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.isLooping;
        }

        PlaySound("MainTheme");
    }


    public void PlaySound(string name)
    {
        foreach(Sound s in sounds)
        {
            if(s.soundName == name)
            {
                s.source.Play();
            }
        }   
    }

}
