using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Cutscene", menuName = "Scriptable Objects/Cutscene")]
public class Cutscene : ScriptableObject
{
    [SerializeField] private AudioClip _cutscenMusic;
    public AudioClip CutsceneMusic => _cutscenMusic;
    [SerializeField] private string _endButtonText = "Next";
    public string EndButtonText => _endButtonText;
    
    [SerializeField, ReorderableList] private List<CutsceneBlock> _cutsceneBlocks;

    public List<CutsceneBlock> GetCutsceneBlocks() => new List<CutsceneBlock>(_cutsceneBlocks);
}
