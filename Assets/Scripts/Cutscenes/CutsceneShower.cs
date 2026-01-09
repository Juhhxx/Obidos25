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
    [SerializeField] private PlaySound _soundPlayer;

    [SerializeField] private float _textSpeed;
    [SerializeField] private float _imageFadeSpeed;

    private YieldInstruction _wfs;
    private YieldInstruction _wff;

    private bool _nextPressed = false;

    private void Start()
    {
        _wfs = new WaitForSeconds(_textSpeed);
        _wff = new WaitForEndOfFrame();

        _nextButton.onClick.AddListener(NextText);
    }

    public void NextText() => _nextPressed = true;

    public void ShowCutsceneBlock(CutsceneBlock cutsceneBlock)
    {
        StopAllCoroutines();
        StartCoroutine(ShowCutsceneBlockCR(cutsceneBlock));
    }

    private IEnumerator ShowCutsceneBlockCR(CutsceneBlock cutsceneBlock)
    {
        _cutsceneImage.sprite = cutsceneBlock.CutsceneImage;

        Queue<string> textQueue = new Queue<string>(cutsceneBlock.CutsceneTexts);
        int size = textQueue.Count;
        int i = 0;

        while (i < size)
        {
            _nextButton.gameObject.SetActive(false);

            string text = textQueue.Dequeue();

            _cutsceneTmp.text = "";

            foreach (char c in text)
            {
                yield return _wfs;

                _cutsceneTmp.text += c;

                _soundPlayer?.SoundPlay();
            }

            _nextButton.gameObject.SetActive(true);

            yield return _wff;
            yield return new WaitUntil(() => _nextPressed);

            _nextPressed = false;

            i++;
        }
    }
}
 