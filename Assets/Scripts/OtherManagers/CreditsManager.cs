using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System;

public class CreditsManager : MonoBehaviourSingleton<CreditsManager>
{
    [SerializeField] private float _titleDelay;
    [SerializeField] private float _closeDelay;
    [SerializeField] private KeyCode _quitButton;
    [SerializeField] private KeyCode _fastButton;
    [SerializeField] private float _fastSpeed = 3f;

    private Animator _creditsObjectAnim;
    private Animator _creditsAnim;
    private bool _creditsPlaying = false;

    private void Awake()
    {
        base.SingletonCheck(this, true);
    }

    private void Start()
    {
        _creditsObjectAnim  = GetComponent<Animator>();

        var tmp = GetComponentsInChildren<Animator>();
        _creditsAnim = tmp[1];
    }

    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public void OpenCredits() => OpenCredits(true);

    public void OpenCredits(bool doFade = true)
    {
        if (doFade) _creditsObjectAnim.SetTrigger("Open");
        else _creditsObjectAnim.SetTrigger("OpenNoFade");
    }

    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public void StartCredits()
    {
        _creditsPlaying = true;
        _creditsAnim.SetTrigger("Roll");
    }

    public void ShowTitle()
    {
        StartCoroutine(ShowTitleCR());
    }

    private IEnumerator ShowTitleCR()
    {
        _creditsPlaying = false;
        Time.timeScale = 1.00f;

        yield return new WaitForSeconds(_titleDelay);

        _creditsAnim.SetTrigger("Title");
    }

    public void CloseCredits()
    {
        StartCoroutine(CloseCreditsCR());
    }

    private IEnumerator CloseCreditsCR()
    {
        yield return new WaitForSeconds(_closeDelay);

        _creditsObjectAnim.SetTrigger("Close");
    }

    private void CreditFastRollCheck()
    {
        if (Input.GetKey(_fastButton))
        {
            Time.timeScale = _fastSpeed;
        }
        else
        {
            Time.timeScale = 1.00f;
        }
    }

    private void QuitCreditsCheck()
    {
        if (Input.GetKeyDown(_quitButton))
        {
            QuitCredits();
        }
    }

    public void QuitCredits()
    {
        _creditsObjectAnim.SetTrigger("Quit");
        _creditsAnim.SetTrigger("Stop");
    }

    private void Update()
    {
        if (_creditsPlaying)
        {
            CreditFastRollCheck();
            QuitCreditsCheck();
        }
    }
}
