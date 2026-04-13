using UnityEngine;

public class AudioEvent : MonoBehaviourSingletonPersistent<AudioEvent>
{
    [Header("UI")]
    [SerializeField] public AudioClip _buttonClicked;

    [Header("Musics")]
    [SerializeField] public AudioClip _menuMusic;

    [Header("Shoot")]
    [SerializeField] public AudioClip _shoot1;
    [SerializeField] public AudioClip _shoot2;

    [Header("Shield")]
    [SerializeField] public AudioClip _Shield;

    [Header("Melee")]
    [SerializeField] public AudioClip _melee;

}
