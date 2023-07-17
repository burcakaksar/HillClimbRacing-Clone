using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource carSound;
    public AudioSource environmentSound;
    public AudioClip[] sounds;
    public AudioClip[] carSounds;

    public static SoundManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PlayWithIndex(int index)
    {
        environmentSound.PlayOneShot(sounds[index]);
    }
    public void PlayCarSound(int index)
    {
        carSound.clip = carSounds[index];
        carSound.loop = true;
        carSound.Play();
    }
}
