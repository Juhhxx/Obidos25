using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviourSingleton<SceneChanger>
{
    private Animator _anim;

    private void Awake()
    {
        base.SingletonCheck(this, true);
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }


    public void ChangeScene(string scene)
    {
        StartCoroutine(ChangeSceneCR(scene));
    }

    private IEnumerator ChangeSceneCR(string scene)
    {
        _anim.SetTrigger("FadeOut");

        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => !AnimatorIsPlaying(_anim));

        SceneManager.LoadScene(scene);
;
        _anim.SetTrigger("FadeIn");
    }

    private bool AnimatorIsPlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
