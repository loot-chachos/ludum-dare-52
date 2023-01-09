using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tool;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource = null;
    [SerializeField] private GameObject _sourcePrefab = null;

    [Header("Musics")]
    [SerializeField] private AudioClip _mainMenuMusic = null;
    [SerializeField] private AudioClip _dialogMusic = null;
    [SerializeField] private AudioClip _inGameMusic = null;

    [Header("SFX")]
    [SerializeField] private AudioClip _shootSound = null;
    [SerializeField] private AudioClip _watered1Sound = null;
    [SerializeField] private AudioClip _watered2Sound = null;

    private List<AudioSource> _audioPool = null;


    public void Awake()
    {
        _audioPool = new List<AudioSource>();
        _musicAudioSource.loop = true;
    }

    #region Musics
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
    #endregion Musics

    #region Sounds
    private AudioSource GetAvailableAudioSource()
    {
        AudioSource finalSource = null;
        int index = -1;
        bool found = false;
        for (int i = 0; i < _audioPool.Count && !found; i++)
        {
            if (_audioPool[i].isPlaying == false)
            {
                found = true;
                finalSource = _audioPool[i];
            }
        }

        if (!found)
        {
            AudioSource newSource = Instantiate(_sourcePrefab).GetComponent<AudioSource>();
            _audioPool.Add(newSource);
            finalSource = newSource;
        }

        return finalSource;
    }

    public void PlayShoot()
    {
        AudioSource source = GetAvailableAudioSource();
        source.PlayOneShot(_shootSound);
    }

    public void PlayHarvest()
    {
        AudioSource source = GetAvailableAudioSource();
        source.PlayOneShot(_watered1Sound);
    }

    public void PlayTool(ToolType type)
    {
        if (type == ToolType.Others)
        {
            AudioSource source = GetAvailableAudioSource();
            source.PlayOneShot(_watered2Sound);
        }
        else
        {

        }
    }
    #endregion Sounds
}
