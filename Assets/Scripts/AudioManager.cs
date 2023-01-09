using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource = null;

    [Header("Musics")]
    [SerializeField] private AudioClip _mainMenuMusic = null;
    [SerializeField] private AudioClip _dialogMusic = null;
    [SerializeField] private AudioClip _inGameMusic = null;

    [Header("SFX")]
    [SerializeField] private AudioClip _shootSound = null;
    [SerializeField] private AudioClip _watered1Sound = null;
    [SerializeField] private AudioClip _watered2Sound = null;


    public void Awake()
    {
        _musicAudioSource.loop = true;
    }

    public void PlayMenuMusic()
    {
        _musicAudioSource.clip = _mainMenuMusic;
        _musicAudioSource.Play();
    }

    public void PlayDialogMusic()
    {
        _musicAudioSource.clip = _dialogMusic;
        _musicAudioSource.Play();
    }

    public void PlayInGameMusic()
    {
        _musicAudioSource.clip = _inGameMusic;
        _musicAudioSource.Play();
    }
}
