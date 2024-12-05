using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip click;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ClickAudio()
    {
        audioSource.PlayOneShot(click);
    }
}


