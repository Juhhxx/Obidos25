using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;

public class CutsceneShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cutsceneTmp;
    [SerializeField] private Image _cutsceneImageFade;
    [SerializeField] private Image _cutsceneImageStatic;
    [SerializeField] private ImageMode _imageMode = ImageMode.FadeInOut;
    public enum ImageMode { FadeInOut, FadeToImage }

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

    private const string HTML_ALPHA = "<color=#00000000>";

    private void Start()
    {
        _wfs = new WaitForSeconds(_textSpeed);
        _wff = new WaitForEndOfFrame();

        _nextButton.onClick.AddListener(NextText);
        _nextButton.gameObject.SetActive(false);
        _skipButton.onClick.AddListener(SkipCutscene);
        _cutsceneTmp.text = "";
        _cutsceneImageStatic.color = new Color(1f, 1f, 1f, 0f);

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

    public void ShowCutsceneBlock(CutsceneBlock cutsceneBlock, bool lastBlock = false, string endBttText = "")
    {
        StopAllCoroutines();
        StartCoroutine(ShowCutsceneBlockCR(cutsceneBlock, lastBlock, endBttText));
    }

    private IEnumerator ShowCutsceneBlockCR(CutsceneBlock cutsceneBlock, bool lastBlock = false, string endBttText = "")
    {
        _isShowing = true;

        _cutsceneImageStatic.gameObject.SetActive(_imageMode == ImageMode.FadeToImage);

        _nextButton.gameObject.SetActive(false);
        _cutsceneImageFade.color = new Color(1f, 1f, 1f, 0f);
        _cutsceneTmp.text = "";

        yield return StartCoroutine(FadeGroupCR(1f));

        _cutsceneImageFade.sprite = cutsceneBlock.CutsceneImage;
        yield return StartCoroutine(FadeImageCR(1f));

        Queue<string> textQueue = new Queue<string>(cutsceneBlock.CutsceneTexts);
        int size = textQueue.Count;
        int i = 0;

        while (i < size)
        {
            string text = textQueue.Dequeue();
            string displayText = "";
            int alphaIndex = 0;

            bool inCode = false;

            foreach (char c in text)
            {
                alphaIndex++;

                if (c == '<') inCode = true;

                if (inCode)
                {
                    if (c == '>') inCode = false;

                    continue;
                }

                displayText = text.Insert(alphaIndex, HTML_ALPHA);

                _cutsceneTmp.text = displayText;

                _soundPlayer?.SoundPlay();

                yield return _wfs;
            }

            _nextButton.gameObject.SetActive(true);

            if (lastBlock && i == size - 1)
            {
                Debug.Log($"LAST BLOCK SPECIAL TEXT {endBttText}", this);
                _nextButton.GetComponentInChildren<TextMeshProUGUI>().text = endBttText;
            }

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

        if (lastBlock)
        {
            _cutsceneImageStatic.gameObject.SetActive(false);
            
            yield return StartCoroutine(FadeGroupCR(0f));
        }
        else
        {
            if (_imageMode == ImageMode.FadeInOut) yield return StartCoroutine(FadeImageCR(0f));
            else
            {
                _cutsceneImageStatic.sprite = cutsceneBlock.CutsceneImage;
                _cutsceneImageStatic.color = Color.white;
            }
        }

        _isShowing = false;
        Debug.LogWarning("DONE SHOWING BLOCK", this);
    }

    private IEnumerator FadeImageCR(float targetAlpha)
    {
        float t = 0f;

        while (!Mathf.Approximately(_cutsceneImageFade.color.a, targetAlpha))
        {
            Color newColor = _cutsceneImageFade.color;

            newColor.a = Mathf.MoveTowards(_cutsceneImageFade.color.a, targetAlpha, t);

            _cutsceneImageFade.color = newColor;

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
 