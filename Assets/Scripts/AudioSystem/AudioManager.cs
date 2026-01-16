using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private PlaySound _soundTestMaster;
    [SerializeField] private PlaySound _soundTestMusic;
    [SerializeField] private PlaySound _soundTestAmbience;
    [SerializeField] private PlaySound _soundTestSFX;

    private const string MASTERVOLUME = "masterVolume";
    private const string MUSICVOLUME = "musicVolume";
    private const string AMBIENCEVOLUME = "ambienceVolume";
    private const string SFXVOLUME = "sfxVolume";

    private Dictionary<AudioGroup, AudioMixerGroup> _audioGroups = new Dictionary<AudioGroup, AudioMixerGroup>();

    private void LocateAudioGroups()
    {
        AudioMixerGroup[] mixerGroups = _audioMixer.FindMatchingGroups("");

        foreach (AudioGroup ag in Enum.GetValues(typeof(AudioGroup)))
        {
            foreach (AudioMixerGroup amg in mixerGroups)
            {
                if (amg.name == ag.ToString()) _audioGroups.Add(ag, amg);
            }
        }

        Debug.LogWarning("[AudioManager] Associating AudioMixerGroups to Enum :", this);

        foreach (KeyValuePair<AudioGroup, AudioMixerGroup> kv in _audioGroups)
        {
            Debug.LogWarning($"{kv.Key.ToString()} : {kv.Value.name}", this);
        }

        Debug.LogWarning("-", this);
    }

    public AudioMixerGroup GetAudioMixerGroup(AudioGroup group) => _audioGroups[group];

    [SerializeField, ReadOnly] private float _masterVolume;
    public float MasterVolume
    {
        get => _masterVolume;

        set
        {
            if (value != _masterVolume)
            {
                _audioMixer.SetFloat(MASTERVOLUME, ValueToDb(value));
                PlayerPrefs.SetFloat(MASTERVOLUME, value);
                PlayerPrefs.Save();

                if (MenuManager.Instance.OptionsOpen)
                    _soundTestMaster.SoundPlay();

                _masterVolume = value;
            }

            _masterVolume = value;
        }
    }

    [SerializeField, ReadOnly] private float _musicVolume;
    public float MusicVolume
    {
        get => _musicVolume;

        set
        {
            if (value != _musicVolume)
            {
                _audioMixer.SetFloat(MUSICVOLUME, ValueToDb(value));
                PlayerPrefs.SetFloat(MUSICVOLUME, value);
                PlayerPrefs.Save();

                if (MenuManager.Instance.OptionsOpen)
                    _soundTestMusic.SoundPlay();

                _musicVolume = value;
            }

            _musicVolume = value;
        }
    }

    [SerializeField, ReadOnly] private float _ambienceVolume;
    public float AmbienceVolume
    {
        get => _ambienceVolume;

        set
        {
            if (value != _ambienceVolume)
            {
                _audioMixer.SetFloat(AMBIENCEVOLUME, ValueToDb(value));
                PlayerPrefs.SetFloat(AMBIENCEVOLUME, value);
                PlayerPrefs.Save();

                if (MenuManager.Instance.OptionsOpen)
                    _soundTestAmbience.SoundPlay();

                _ambienceVolume = value;
            }

            _ambienceVolume = value;
        }
    }

    [SerializeField, ReadOnly] private float _sfxVolume;
    public float SFXVolume
    {
        get => _sfxVolume;

        set
        {
            if (value != _sfxVolume)
            {
                _audioMixer.SetFloat(SFXVOLUME, ValueToDb(value));
                PlayerPrefs.SetFloat(SFXVOLUME, value);
                PlayerPrefs.Save();

                if (MenuManager.Instance.OptionsOpen)
                    _sfxVolume = value;

                _soundTestSFX.SoundPlay();
            }

            _sfxVolume = value;
        }
    }

    private float ValueToDb(float value)
    {
        if (value <= 0f)
            return -80f;

        return Mathf.Log10(value) * 20f;
    }

    public event Action OnResetValues;

    [Serializable]
    public struct AudioSourceInfo
    {
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public AudioGroup MixerGroup { get; private set; }

        public AudioSourceInfo(AudioSource source, AudioGroup group)
        {
            AudioSource = source;
            MixerGroup = group;
        }
    }

    [SerializeField, ReadOnly] private List<AudioSourceInfo> _activeAudioSources = new List<AudioSourceInfo>();

    public void RegisterAudioSource(AudioSource source, AudioGroup group)
    {
        foreach (AudioSourceInfo asi in _activeAudioSources)
        {
            if (asi.AudioSource == source) return;
        }

        _activeAudioSources.Add(new AudioSourceInfo(source, group));
    }

    public void UnRegisterAudioSource(AudioSource source)
    {
        foreach (AudioSourceInfo asi in _activeAudioSources)
        {
            if (asi.AudioSource == source)
            {
                _activeAudioSources.Remove(asi);
                break;
            }
        }
    }

    private void ClearAllAudioSources()
    {
       _activeAudioSources.Clear();
    }

    [field: SerializeField] public AudioSoundPlayer SoundPlayer { get; private set; }
    [field: SerializeField] public AudioMusicPlayer MusicPlayer { get; private set; }

    private void Awake()
    {
        base.SingletonCheck(this, true);

        LocateAudioGroups();
        SceneManager.sceneLoaded += (scene, mode) => ClearAllAudioSources();
    }

    private void Start()
    {
        LoadVolumeValues();
    }

    [Button(enabledMode: EButtonEnableMode.Always)]
    private void ResetVolumes()
    {
        MasterVolume    = 1.0f;
        MusicVolume  = 1.0f;
        AmbienceVolume  = 1.0f;
        SFXVolume       = 1.0f;

        OnResetValues?.Invoke();
    }

    public void ToggleMuteInGroup(AudioGroup group, bool onOff)
    {
        switch (group)
        {
            case AudioGroup.Master:
                ToggleMute(MASTERVOLUME, onOff);
                break;
            
            case AudioGroup.Music:
                ToggleMute(MUSICVOLUME, onOff);
                break;
            
            case AudioGroup.Ambience:
                ToggleMute(AMBIENCEVOLUME, onOff);
                break;
            
            case AudioGroup.SFX:
                ToggleMute(SFXVOLUME, onOff);
                break;
        }
    }

    private void ToggleMute(string group, bool onOff)
    {
        float volume = onOff ? PlayerPrefs.GetFloat(group) : 0f;

        _audioMixer.SetFloat(group, volume);
    }

    public void TogglePauseGroup(AudioGroup group, bool onOff)
    {
        foreach (AudioSourceInfo asi in _activeAudioSources)
        {
            if (asi.MixerGroup == group)
            {
                if (onOff)
                    asi.AudioSource.Pause();
                else
                    asi.AudioSource.UnPause();
            }
        }
    }

    public void TogglePauseAllGroups(bool onOff)
    {
        TogglePauseGroup(AudioGroup.Master, onOff);
        TogglePauseGroup(AudioGroup.Music, onOff);
        TogglePauseGroup(AudioGroup.Ambience, onOff);
        TogglePauseGroup(AudioGroup.SFX, onOff);
    }

    private void LoadVolumeValues()
    {
        MasterVolume    = PlayerPrefs.GetFloat(MASTERVOLUME);
        MusicVolume     = PlayerPrefs.GetFloat(MUSICVOLUME);
        AmbienceVolume  = PlayerPrefs.GetFloat(AMBIENCEVOLUME);
        SFXVolume       = PlayerPrefs.GetFloat(SFXVOLUME);

        if (MasterVolume == 0)
        {
            Debug.Log("[Audio Manager] No Audio Preferences Found, Setting Values to Default.", this);
            MasterVolume = 1;
            MusicVolume = 1;
            AmbienceVolume = 1;
            SFXVolume = 1;
        }
    }
    
}
