using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int _sceneToLoad = -1;
    [SerializeField] private Animator _fadeAnimator;

    public void LoadScene(int index = -1)
    {
        if (index != -1)
            StartCoroutine(LoadSceneCoroutine(index));
        else if (_sceneToLoad != -1)
            StartCoroutine(LoadSceneCoroutine(_sceneToLoad));
        else
            Debug.LogError("Start To Load Scene FAIL !");

    }

    IEnumerator LoadSceneCoroutine(int index)
    {
        _fadeAnimator.SetTrigger("Fade");

        yield return null; 

        float duration = _fadeAnimator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(index);
    }
}
