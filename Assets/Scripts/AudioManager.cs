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
    [SerializeField] private AudioClip _harvest = null;
    [SerializeField] private AudioClip _water = null;
    [SerializeField] private AudioClip _fertilze = null;
    [SerializeField] private AudioClip _seed = null;
    [SerializeField] private AudioClip _shovel = null;

    private List<AudioSource> _audioPool = null;

    private float _timer = 0.0f;

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
        source.PlayOneShot(_harvest);
    }

    public void PlayTool(ToolType type)
    {
        _timer += Time.deltaTime;
        if (_timer <= 0.10f)
        {
            return;
        }

        _timer = 0.0f;
        switch (type)
        {
            case ToolType.Seed:
                {
                    AudioSource source = GetAvailableAudioSource();
                    source.PlayOneShot(_seed);
                }
                break;
            case ToolType.Water:
                {
                    AudioSource source = GetAvailableAudioSource();
                    source.PlayOneShot(_water);
                }
                break;
            case ToolType.Fertilzer:
                {
                    AudioSource source = GetAvailableAudioSource();
                    source.PlayOneShot(_fertilze);
                }
                break;
            case ToolType.Shovel:
                {
                    AudioSource source = GetAvailableAudioSource();
                    source.PlayOneShot(_shovel);
                }
                break;
            case ToolType.Others:
                break;
        }
    }
    #endregion Sounds
}
