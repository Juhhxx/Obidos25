using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soudsToPlay;
    [SerializeField] private AudioGroup _group;
    [SerializeField] [Range (0, 1)] private float _volume;
    [SerializeField, MinMaxSlider(0,3)] private Vector2 _pitchRange;

    [SerializeField] private bool _changePitch;
    [SerializeField] private bool _playOnStart;
    [SerializeField] private bool _loop;

    AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.loop = _loop;

        if (_playOnStart) SoundPlay();
    }

    public void SoundPlay()
    {
        Debug.Log("SOUND");

        int soundIdx = 0;

        if (_soudsToPlay.Length > 1)
        {
            soundIdx = Random.Range(0,_soudsToPlay.Length);
        }

        AudioClip clip = _soudsToPlay[soundIdx];

        float pitch = 1f;

        if (_changePitch)
        {
            pitch = Random.Range(_pitchRange.x,_pitchRange.y);
        }

        Debug.Log($"Playing {clip.name} at pitch {pitch} in group {_group}");

        if (!_audioSource.isPlaying)    
            AudioManager.Instance.SoundPlayer.PlayClipExisting(_audioSource, clip, _group, _volume, pitch);
    }
}
