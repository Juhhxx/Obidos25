using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using System.Collections;
using System.Linq;
using System;

public class CutsceneManager : MonoBehaviourSingleton<CutsceneManager>
{
    [SerializeField] private Cutscene _cutscene;
    [SerializeField] private CutsceneShower _cutsceneShower;
    [SerializeField] private GameObject _cutsceneCanvas;

    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public void Play() => PlayCutscene(_cutscene);

    public void ResetButtonSelection() => MenuManager.Instance.ResetSelection();

    private void Awake()
    {
        base.SingletonCheck(this, false);
    }

    public void PlayCutscene(Cutscene cutscene, Action onFinished = null)
    {
        StopAllCoroutines();
        StartCoroutine(PlayCutsceneCR(cutscene, onFinished));
    }

    private IEnumerator PlayCutsceneCR(Cutscene cutscene, Action onFinished)
    {
        List<CutsceneBlock> cutsceneBlocks = cutscene.GetCutsceneBlocks();

        _cutsceneCanvas.SetActive(true);

        MenuManager.Instance.CanPause = false;

        foreach (CutsceneBlock block in cutsceneBlocks)
        {
            Debug.LogWarning("PLAYING CUTSCENE BLOCK", this);

            _cutsceneShower.ShowCutsceneBlock(block, block == cutsceneBlocks.Last());

            yield return new WaitUntil(() => !_cutsceneShower.IsShowing);

            if (_cutsceneShower.Skipped)
            {
                Debug.LogWarning("SKIPPING CUTSCENE", this);
                _cutsceneShower.ResetSkip();
                break;
            }
        }

        onFinished?.Invoke();
        
        MenuManager.Instance.CanPause = true;

        _cutsceneCanvas.SetActive(false);
    }
}
