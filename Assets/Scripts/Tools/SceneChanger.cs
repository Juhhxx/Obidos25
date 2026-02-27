using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviourSingleton<SceneChanger>
{
    private Animator _anim;
    private RawImage _image;

    private void Awake()
    {
        base.SingletonCheck(this, true);
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _image = GetComponentInChildren<RawImage>();
        
        _image.raycastTarget = false;
    }

    public void ChangeScene(string scene, Action onLoad, bool doFade = true)
    {
        StartCoroutine(ChangeSceneCR(scene, onLoad, doFade));
    }

    private IEnumerator ChangeSceneCR(string scene, Action onLoad, bool doFade)
    {
        _image.raycastTarget = true;

        if (doFade) _anim.SetTrigger("FadeOut");

        yield return new WaitForEndOfFrame();
        if (doFade) yield return new WaitUntil(() => !AnimatorIsPlaying(_anim));

        SceneManager.LoadScene(scene);

        onLoad?.Invoke();

        if (doFade) _anim.SetTrigger("FadeIn");

        yield return new WaitUntil(() => !AnimatorIsPlaying(_anim));

        _image.raycastTarget = false;
    }

    private bool AnimatorIsPlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
