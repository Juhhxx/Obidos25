using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetVolumeFromSlider : MonoBehaviour
{
    [SerializeField] private AudioGroup _type;
    private Slider _slider;
    private float Value
    {
        get
        {
            switch (_type)
            {
                case AudioGroup.Master:
                    return AudioManager.Instance.MasterVolume;
                
                case AudioGroup.Music:
                    return AudioManager.Instance.MusicVolume;

                case AudioGroup.Ambience:
                    return AudioManager.Instance.AmbienceVolume;
                
                case AudioGroup.SFX:
                    return AudioManager.Instance.SFXVolume;

                default:
                    return 0;
            }
        }

        set
        {
            switch (_type)
            {
                case AudioGroup.Master:
                    AudioManager.Instance.MasterVolume = value;
                    break;
                
                case AudioGroup.Music:
                    AudioManager.Instance.MusicVolume = value;
                    break;

                case AudioGroup.Ambience:
                    AudioManager.Instance.AmbienceVolume = value;
                    break;
                
                case AudioGroup.SFX:
                    AudioManager.Instance.SFXVolume = value;
                    break;
            }
        }
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();

        UpdateSlider();

        AudioManager.Instance.OnResetValues += UpdateSlider;
    }

    private void OnDestroy()
    {
        if (AudioManager.Instance == null) return;
        
        AudioManager.Instance.OnResetValues -= UpdateSlider;
    }

    public void SetVolume()
    {
        Value = _slider.value;
    }

    public void UpdateSlider()
    {
        _slider.value = Value;
    }
}
