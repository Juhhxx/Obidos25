using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Cutscene _cutscene;
    [SerializeField] private CutsceneShower _cutsceneShower;


    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public void Play() => PlayCutscene(_cutscene);

    public void PlayCutscene(Cutscene cutscene)
    {
        _cutsceneShower.ShowCutsceneBlock(cutscene.GetCutsceneBlocks()[0]);
    }

}
