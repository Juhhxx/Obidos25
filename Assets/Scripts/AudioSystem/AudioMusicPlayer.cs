using UnityEngine;
using System.Collections;

public class AudioMusicPlayer : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float _fadeDuration = 1.5f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private AudioGroup group = AudioGroup.Ambience;

    private AudioSource _sourceA;
    private AudioSource _sourceB;
    private AudioSource _currentSource;
    private Coroutine _fadeCoroutine;

    public void SetUp()
    {
        _sourceA = gameObject.AddComponent<AudioSource>();
        _sourceB = gameObject.AddComponent<AudioSource>();

        _sourceA.loop = true;
        _sourceB.loop = true;

        _sourceA.outputAudioMixerGroup = AudioManager.Instance.GetAudioMixerGroup(group);
        _sourceB.outputAudioMixerGroup = AudioManager.Instance.GetAudioMixerGroup(group);

        AudioManager.Instance.RegisterAudioSource(_sourceA, group, true);
        AudioManager.Instance.RegisterAudioSource(_sourceB, group, true);

        _currentSource = _sourceA;
    }

    /// <summary>
    /// Plays a song. If another song is playing, crossfades between them.
    /// </summary>
    public void PlaySong(AudioClip newClip)
    {
        if (newClip == null)
        {
            StopMusic();
            return;
        }

        if (_currentSource.clip != null)
            if (_currentSource.clip == newClip && _currentSource.isPlaying) return;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        AudioSource nextSource = (_currentSource == _sourceA) ? _sourceB : _sourceA;

        nextSource.clip = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        _fadeCoroutine = StartCoroutine(Crossfade(_currentSource, nextSource));
        _currentSource = nextSource;
    }

    private IEnumerator Crossfade(AudioSource from, AudioSource to)
    {
        float t = 0f;

        float fromStartVolume = from != null ? from.volume : 0f;

        while (t < _fadeDuration)
        {
            t += Time.deltaTime;
            float normalized = t / _fadeDuration;

            if (from != null)
                from.volume = Mathf.Lerp(fromStartVolume, 0f, normalized);

            to.volume = Mathf.Lerp(0f, _maxVolume, normalized);

            yield return null;
        }

        if (from != null)
        {
            from.Stop();
            from.clip = null;
        }

        to.volume = _maxVolume;
    }

    /// <summary>
    /// Stops music with fade-out.
    /// </summary>
    public void StopMusic()
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeOut(_currentSource));
    }

    private IEnumerator FadeOut(AudioSource source)
    {
        if (source == null || !source.isPlaying)
            yield break;

        float startVolume = source.volume;
        float t = 0f;

        while (t < _fadeDuration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, t / _fadeDuration);
            yield return null;
        }

        source.Stop();
        source.clip = null;
    }
}
