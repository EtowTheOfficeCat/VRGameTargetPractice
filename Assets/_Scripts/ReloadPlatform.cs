using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadPlatform : MonoBehaviour
{

    public AudioSource m_ReloadSoudSource;
    public AudioClip m_ReloadClip;
    
    void Start()
    {
        m_ReloadSoudSource.clip = m_ReloadClip;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Phaser"))
            m_ReloadSoudSource.Play();
            
    }
}
