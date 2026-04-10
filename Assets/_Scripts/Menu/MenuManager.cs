using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    AudioManager _audioManager;
    AudioEvent _audioEvent;

    public void Start()
    {
        if (AudioManager.Instance != null)
            _audioManager = AudioManager.Instance;

        if (AudioEvent.Instance != null)
            _audioEvent = AudioEvent.Instance;

        _audioManager.PlayMusic(_audioEvent._menuMusic);
    }

    #region Play

    [SerializeField] SceneLoader _sceneLoader;

    public void Play()
    {
        _sceneLoader.LoadScene(1);
    }
    #endregion

    #region Parameters

    [SerializeField] GameObject _parameters;

    public void Parameters(bool active)
    {
        StartCoroutine(ParametersCoroutine(active));
    }
    IEnumerator ParametersCoroutine(bool active)
    {
        _parameters.SetActive(active);

        _audioManager.PlayOneShot(_audioEvent._buttonClicked);

        yield return null;
    }

    #endregion

    #region Credits

    [SerializeField] GameObject _credits;

    public void Credits(bool active) 
    {
        StartCoroutine(CreditsCoroutine(active));
    }

    IEnumerator CreditsCoroutine(bool active)
    {
        _credits.SetActive(active);

        _audioManager.PlayOneShot(_audioEvent._buttonClicked);

        yield return null;
    }
    #endregion
}
