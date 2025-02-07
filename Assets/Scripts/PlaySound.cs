using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soudsToPlay;
    [SerializeField] [Range (0, 1)] private float _volume;
    [SerializeField] private Vector2 _pitchRange;
    [SerializeField] private bool _isVoice;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = _volume;
    }
    public void SoundPlay()
    {
        Debug.Log("SOUND");

        int soundIdx = 0;

        if (_soudsToPlay.Length > 1)
        {
            soundIdx = Random.Range(0,_soudsToPlay.Length);
        }

        _audioSource.clip = _soudsToPlay[soundIdx];

        if (_isVoice)
        {
            _audioSource.pitch = Random.Range(_pitchRange.x,_pitchRange.y);
        }

        Debug.Log($"Playing {_audioSource.clip.name} at pitch {_audioSource.pitch}");

        if (!_audioSource.isPlaying && !_isVoice)    
            _audioSource.Play();
        else
            _audioSource.Play();
    }
}
