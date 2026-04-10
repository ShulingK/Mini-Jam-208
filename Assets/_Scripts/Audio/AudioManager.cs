using UnityEngine;

public class AudioManager : MonoBehaviourSingletonPersistent<AudioManager>
{
    private const string MUSIC_KEY = "MusicVolume";
    private const string SFX_KEY = "SFXVolume";

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSfx;

    private void Start()
    {
        LoadVolumes();
    }

    private void LoadVolumes()
    {
        float music = PlayerPrefs.GetFloat(MUSIC_KEY, 0.2f);
        float sfx = PlayerPrefs.GetFloat(SFX_KEY, 0.2f);

        SetMusicVolume(music);
        SetSFXVolume(sfx);
    }

    public void SetMusicVolume(float volume)
    {
        _audioSourceMusic.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }

    public float GetMusicVolume() => _audioSourceMusic.volume;

    public void SetSFXVolume(float volume)
    {
        _audioSourceSfx.volume = volume;
        PlayerPrefs.SetFloat(SFX_KEY, volume);
    }

    public float GetSFXVolume() => _audioSourceSfx.volume;

    public void PlayOneShot(AudioClip sound)
    {
        _audioSourceSfx.PlayOneShot(sound);
    }

    public void PlayMusic(AudioClip sound)
    {
        _audioSourceMusic.clip = sound;
        _audioSourceMusic.Play();
    }
}