using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private AnimationAlpha animationAlpha;
    [SerializeField] private Image image;

    private Coroutine coroutineLevelTransition;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    public void SetScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void SetSceneTransition(string newScene)
    {
        if (coroutineLevelTransition != null)
            StopCoroutine(coroutineLevelTransition);

        coroutineLevelTransition = StartCoroutine(CoroutineLevelTransition(newScene));
    }

    private IEnumerator CoroutineLevelTransition(string newScene)
    {
        animationAlpha.StartAnimationAlpha(0, 0.2f);

        SetScene(newScene);
        yield return null;
    }
}
