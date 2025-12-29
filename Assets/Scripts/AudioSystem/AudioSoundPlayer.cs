using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class AudioSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourcePrefab;

    private AudioSource CreateAudiOSource(Vector3 position, AudioGroup group)
    {
        AudioSource audioSource = Instantiate(_audioSourcePrefab, position, Quaternion.identity);

        AudioManager.Instance.RegisterAudioSource(audioSource, group);

        return audioSource;
    }

    private IEnumerator DestroyAudioSourceCR(AudioSource source)
    {
        yield return new WaitUntil(() => !source.isPlaying);

        AudioManager.Instance.UnRegisterAudioSource(source);

        Destroy(source);
    }

    public void PlayClip(AudioClip clip, AudioGroup group, Vector3 position, float volume = 1f, float pitch = 1f)
    {
        AudioSource audioSource = CreateAudiOSource(position, group);

        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = AudioManager.Instance.GetAudioMixerGroup(group);

        audioSource.Play();

        DestroyAudioSourceCR(audioSource);
    }

    public AudioSource PlayClipReturn(AudioClip clip, AudioGroup group, Vector3 position, float volume = 1f, float pitch = 1f)
    {
        AudioSource audioSource = CreateAudiOSource(position, group);

        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = AudioManager.Instance.GetAudioMixerGroup(group);

        audioSource.Play();

        return audioSource;
    }

    public void PlayClipExisting(AudioSource audioSource, AudioClip clip, AudioGroup group, float volume = 1f, float pitch = 1f)
    {
        AudioManager.Instance.RegisterAudioSource(audioSource, group);

        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = AudioManager.Instance.GetAudioMixerGroup(group);

        audioSource.Play();
    }


}
