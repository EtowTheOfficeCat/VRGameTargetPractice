using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip Hover;
    [SerializeField] private AudioClip Click;

    public void HoverSound()
    {
        AudioSource.PlayOneShot(Hover);
    }

    public void ClickSound()
    {
        AudioSource.PlayOneShot(Click);
    }
}
