using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource oneShotSFXSource;
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private float defaultVolume = 0.3f;
    [SerializeField] private AudioClip[] playerSounds;

    private Coroutine delayBgMusicHandle;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    private void Start()
    {
        sFXSource.volume = defaultVolume;
        backgroundMusicSource.volume = 0.2f;
        backgroundMusicSource.Play();
    }

    public void PlayOneShot(AudioClip clip)
    {
        Instance.oneShotSFXSource.PlayOneShot(clip, defaultVolume);
    }

    public void Play(AudioClip clip)
    {
        sFXSource.clip = clip;
        sFXSource.Play();
    }

    // Interrupt the background music with an audio clip.
    // The background music will pause to play the audio clip,
    // and will resume after the audio clip is finished playing
    public void InterruptBgMusic(AudioClip audioClip)
    {
        if (delayBgMusicHandle != null)
        {
            StopCoroutine(delayBgMusicHandle);
            delayBgMusicHandle = null;
        }

        Instance.StopBackgroundMusic();
        Instance.PlayOneShot(audioClip);
        delayBgMusicHandle = StartCoroutine(delayRestartBgMusic(audioClip.length));
    }

    private IEnumerator delayRestartBgMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlayBackgroundMusic();
    }

    public void Stop()
    {
        sFXSource.Stop();
        sFXSource.clip = null;
    }
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusicSource.Play();
    }
}
