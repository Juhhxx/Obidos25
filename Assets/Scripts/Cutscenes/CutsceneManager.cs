using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Cutscene _cutscene;
    [SerializeField] private CutsceneShower _cutsceneShower;
    [SerializeField] private GameObject _cutsceneCanvas;
    [SerializeField] private bool _startOnAwake;

    public UnityEvent OnCutsceneFinished;

    private void Awake()
    {
        if (_startOnAwake)
        {
            PlayCutscene(_cutscene);
        }
    }

    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public void Play() => PlayCutscene(_cutscene);

    public void PlayCutscene(Cutscene cutscene)
    {
        StopAllCoroutines();
        StartCoroutine(PlayCutsceneCR(cutscene));
    }

    private IEnumerator PlayCutsceneCR(Cutscene cutscene)
    {
        List<CutsceneBlock> cutsceneBlocks = cutscene.GetCutsceneBlocks();

        _cutsceneCanvas.SetActive(true);

        foreach (CutsceneBlock block in cutsceneBlocks)
        {
            _cutsceneShower.ShowCutsceneBlock(block, block == cutsceneBlocks.Last());

            yield return new WaitUntil(() => !_cutsceneShower.IsShowing);

            if (_cutsceneShower.Skipped)
            {
                _cutsceneShower.ResetSkip();
                break;
            }
        }

        OnCutsceneFinished?.Invoke();
        
        _cutsceneCanvas.SetActive(false);
    }
}
