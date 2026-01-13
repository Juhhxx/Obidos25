using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class CutsceneShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cutsceneTmp;
    [SerializeField] private Image _cutsceneImage;
    [SerializeField] private CanvasGroup _cutsceneCanvasGroup;
    [SerializeField] private UnityEngine.UI.Button _nextButton;
    [SerializeField] private UnityEngine.UI.Button _skipButton;
    [SerializeField] private PlaySound _soundPlayer;

    [SerializeField] private float _textSpeed;
    [SerializeField] private float _imageFadeSpeed;
    [SerializeField] private float _canvasFadeSpeed;

    private YieldInstruction _wfs;
    private YieldInstruction _wff;

    private bool _nextPressed = false;

    private bool _isShowing = false;
    public bool IsShowing => _isShowing;

    private bool _skipped = false;
    public bool Skipped => _skipped;
    public void ResetSkip() => _skipped = false;

    private void Start()
    {
        _wfs = new WaitForSeconds(_textSpeed);
        _wff = new WaitForEndOfFrame();

        _nextButton.onClick.AddListener(NextText);
        _nextButton.gameObject.SetActive(false);
        _skipButton.onClick.AddListener(SkipCutscene);
        _cutsceneTmp.text = "";

        Debug.LogWarning("SET CUTSCENE SHOWER", this);
    }

    public void NextText() => _nextPressed = true;

    public void SkipCutscene()
    {
        StopAllCoroutines();
        StartCoroutine(SkipCutsceneCR());
    }

    private IEnumerator SkipCutsceneCR()
    {
        yield return FadeGroupCR(0f);

        _isShowing = false;
        _skipped = true;
    }

    public void ShowCutsceneBlock(CutsceneBlock cutsceneBlock, bool lastBlock = false)
    {
        StopAllCoroutines();
        StartCoroutine(ShowCutsceneBlockCR(cutsceneBlock, lastBlock));
    }

    private IEnumerator ShowCutsceneBlockCR(CutsceneBlock cutsceneBlock, bool lastBlock = false)
    {
        _isShowing = true;

        _nextButton.gameObject.SetActive(false);
        _cutsceneImage.color = new Color(1f, 1f, 1f, 0f);
        _cutsceneTmp.text = "";

        _cutsceneImage.sprite = cutsceneBlock.CutsceneImage;
        yield return StartCoroutine(FadeImageCR(1f));

        Queue<string> textQueue = new Queue<string>(cutsceneBlock.CutsceneTexts);
        int size = textQueue.Count;
        int i = 0;

        while (i < size)
        {
            string text = textQueue.Dequeue();

            foreach (char c in text)
            {
                yield return _wfs;

                _cutsceneTmp.text += c;

                _soundPlayer?.SoundPlay();
            }

            _nextButton.gameObject.SetActive(true);

            yield return _wff;
            yield return new WaitUntil(() => _nextPressed);

            _nextButton.gameObject.SetActive(false);

            _cutsceneTmp.text = "";

            _nextPressed = false;

            yield return new WaitForSeconds(0.15f);

            i++;

            Debug.LogWarning("DONE SHOWING SENTENCE", this);
        }

        _nextButton.gameObject.SetActive(false);

        if (lastBlock) yield return StartCoroutine(FadeGroupCR(0f));
        else yield return StartCoroutine(FadeImageCR(0f));

        _isShowing = false;
        Debug.LogWarning("DONE SHOWING BLOCK", this);
    }

    private IEnumerator FadeImageCR(float targetAlpha)
    {
        float t = 0f;

        while (!Mathf.Approximately(_cutsceneImage.color.a, targetAlpha))
        {
            Color newColor = _cutsceneImage.color;

            newColor.a = Mathf.MoveTowards(_cutsceneImage.color.a, targetAlpha, t);

            _cutsceneImage.color = newColor;

            t = _imageFadeSpeed * Time.deltaTime;

            yield return null;
        }

    }

    public void FadeCutscene(float targetAlpha)
    {
        StopAllCoroutines();
        StartCoroutine(FadeGroupCR(targetAlpha));
    }

    private IEnumerator FadeGroupCR(float targetAlpha)
    {
        float t = 0f;

        while (!Mathf.Approximately(_cutsceneCanvasGroup.alpha, targetAlpha))
        {
            float newAlpha = _cutsceneCanvasGroup.alpha;

            newAlpha = Mathf.MoveTowards(_cutsceneCanvasGroup.alpha, targetAlpha, t);

            _cutsceneCanvasGroup.alpha = newAlpha;

            t = _canvasFadeSpeed * Time.deltaTime;

            yield return null;
        }

    }
}
 