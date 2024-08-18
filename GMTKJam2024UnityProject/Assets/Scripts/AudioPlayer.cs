using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{


    public static AudioPlayer audioPlayer;

    [SerializeField] private AudioSource m_AudioSource;

    [SerializeField] private AudioClip upgradeClip;

    [SerializeField] private AudioClip winClip;

    [SerializeField] private AudioClip loseClip;

    [SerializeField] private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        audioPlayer = this;
    }


    public void PlayAudio(AudioClip clip)
    {
        m_AudioSource.pitch = 1f;
        m_AudioSource.PlayOneShot(clip);
    }


    public void PlayUpgradeAudio()
    {
        m_AudioSource.pitch = 1f;
        m_AudioSource.PlayOneShot(upgradeClip);
    }

    public void PlayDeathAudio()
    {
        backgroundMusic.Stop();
        m_AudioSource.pitch = 1f;
        m_AudioSource.PlayOneShot(loseClip);
    }

    public void PlayWinAudio()
    {
        m_AudioSource.pitch = 1f;
        m_AudioSource.PlayOneShot(winClip);
    }

    public void PlayAudioWithRandomPitch(AudioClip clip)
    {
        float pitch = 1f;
        pitch += Random.Range(-0.3f, 0.3f);
        m_AudioSource.pitch = pitch;
        m_AudioSource.PlayOneShot(clip);
    }

}
