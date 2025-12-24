using UnityEngine;
using UnityEngine.UI;

public class SetVolumeFromSlider : MonoBehaviour
{
    public enum VolumeType { Master, Ambience, SFX }

    [SerializeField] private VolumeType _type;
    private Slider _slider;
    private float Value
    {
        get
        {
            switch (_type)
            {
                case VolumeType.Master:
                    return AudioManager.Instance.MasterVolume;

                case VolumeType.Ambience:
                    return AudioManager.Instance.AmbienceVolume;
                
                case VolumeType.SFX:
                    return AudioManager.Instance.SFXVolume;

                default:
                    return 0;
            }
        }

        set
        {
            switch (_type)
            {
                case VolumeType.Master:
                    AudioManager.Instance.MasterVolume = value;
                    break;

                case VolumeType.Ambience:
                    AudioManager.Instance.AmbienceVolume = value;
                    break;
                
                case VolumeType.SFX:
                    AudioManager.Instance.SFXVolume = value;
                    break;
            }
        }
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();

        _slider.value = Value;
    }

    public void SetVolume()
    {
        Value = _slider.value;
    }
}
