using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private AudioMixer _audioMixer;

    private const string MASTERVOLUME = "masterVolume";
    private const string AMBIENCEVOLUME = "ambienceVolume";
    private const string SFXVOLUME = "sfxVolume";

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
            }

            _masterVolume = value;
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

    private void Awake()
    {
        base.SingletonCheck(this, true);

        LoadVolumeValues();
    }

    [Button(enabledMode: EButtonEnableMode.Always)]
    private void ResetVolumes()
    {
        MasterVolume    = 1.0f;
        AmbienceVolume  = 1.0f;
        SFXVolume       = 1.0f;
    }

    private void LoadVolumeValues()
    {
        MasterVolume    = PlayerPrefs.GetFloat(MASTERVOLUME);
        AmbienceVolume  = PlayerPrefs.GetFloat(AMBIENCEVOLUME);
        SFXVolume       = PlayerPrefs.GetFloat(SFXVOLUME);

        if (MasterVolume == 0)
        {
            MasterVolume = 1;
            AmbienceVolume = 1;
            SFXVolume = 1;
        }
    }

    private void Update()
    {
        _audioMixer.GetFloat(MASTERVOLUME, out float vol);
        Debug.Log($"MASTER VOLUME : {vol}");
    }

    
}
