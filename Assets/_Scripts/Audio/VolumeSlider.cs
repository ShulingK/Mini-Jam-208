using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MUSIC,
        SFX
    }

    [SerializeField] VolumeType _volumeType;

    private Slider _volumeSlider;

    private void Awake()
    {
        _volumeSlider = this.GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch (_volumeType)
        {
            case VolumeType.MUSIC:
                _volumeSlider.value = AudioManager.Instance.GetMusicVolume();
                break;
            case VolumeType.SFX:
                _volumeSlider.value = AudioManager.Instance.GetSFXVolume();
                break;
            default:
                Debug.LogWarning("VolumeType non valide");
                break;
        }
    }

    public void OnSliderValueChange()
    {
        switch (_volumeType)
        {
            case VolumeType.MUSIC:
                AudioManager.Instance.SetMusicVolume(_volumeSlider.value);
                break;
            case VolumeType.SFX:
                AudioManager.Instance.SetSFXVolume(_volumeSlider.value);
                break;
            default:
                Debug.LogWarning("VolumeType non valide");
                break;
        }
    }
}
