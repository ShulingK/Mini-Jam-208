using UnityEngine;

public class AudioEvent : MonoBehaviourSingletonPersistent<AudioEvent>
{
    [Header("UI")]
    [SerializeField] public AudioClip _buttonClicked;

    [Header("Musics")]
    [SerializeField] public AudioClip _menuMusic;
}
