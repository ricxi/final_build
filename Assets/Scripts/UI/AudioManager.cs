using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource oneShotSFXSource;
    [SerializeField] private AudioSource oneShotDelaySFXSource;
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private float defaultVolume = 0.3f;
    [SerializeField] private AudioClip[] playerSounds;

    private bool _isOneShotLocked = false;
    private Coroutine _oneShotCooldownCoHandler = null;
    private Coroutine _delayBgMusicHandle;

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

    public void PlayOneShotWithDelay(AudioClip clip)
    {
        if (_isOneShotLocked) return;
        Instance.oneShotDelaySFXSource.PlayOneShot(clip, defaultVolume);
        if (_oneShotCooldownCoHandler != null)
        {
            StopCoroutine(_oneShotCooldownCoHandler);
            _oneShotCooldownCoHandler = null;
        }
        _oneShotCooldownCoHandler = StartCoroutine(OneShotCooldown(clip.length));
    }

    private IEnumerator OneShotCooldown(float duration)
    {
        _isOneShotLocked = true;
        yield return new WaitForSecondsRealtime(duration);
        _isOneShotLocked = false;
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
        if (_delayBgMusicHandle != null)
        {
            StopCoroutine(_delayBgMusicHandle);
            _delayBgMusicHandle = null;
        }

        Instance.StopBackgroundMusic();
        Instance.PlayOneShot(audioClip);
        _delayBgMusicHandle = StartCoroutine(delayRestartBgMusic(audioClip.length));
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
