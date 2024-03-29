using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 用于播放开采动画
public class CharacterAudioPlay : MonoBehaviour
{
    public AudioClip        m_audio;
    private AudioSource     m_audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
    

    public void PlayCharacterAudio()
    {
        m_audioSource.clip = m_audio;
        m_audioSource.Play();
    }

    public void StopCharacterAudio()
    {
        m_audioSource.Stop();
    }
}
