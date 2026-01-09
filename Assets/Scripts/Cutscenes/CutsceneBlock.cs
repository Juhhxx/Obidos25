using UnityEngine;
using System;
using NaughtyAttributes;
using System.Collections.Generic;

[Serializable]
public class CutsceneBlock
{
    [field: SerializeField] public Sprite CutsceneImage { get; private set; }

    [field: AllowNesting]
    [field: SerializeField, ResizableTextArea] public List<string> CutsceneTexts { get; private set; }
}
