using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

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

        yield return null;
    }
    #endregion
}
