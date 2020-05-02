using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip death, jump;


    public static SoundManager instance=null;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
    public void deathSound()
    {
        source.clip = null;
        source.clip = death;
        source.Play();

    }

    public void stairSound()
    {
        source.clip = null;
        source.clip = jump;
        source.Play();
    }
}
