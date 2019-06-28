using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip Hover;
    public AudioClip Click;

    public void HoverSound()
    {
        AudioSource.PlayOneShot(Hover);
    }

    public void ClickSound()
    {
        AudioSource.PlayOneShot(Click);
    }
}
