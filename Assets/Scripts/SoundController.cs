using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }
    private AudioSource m_AudioSource;

    [SerializeField]
    private AudioClip [] m_AudioClips;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int position)
    {
        m_AudioSource.clip = m_AudioClips[position];
        m_AudioSource.Play();
    }
    
}
