using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public struct DetailInfo
{
    [field: SerializeField] public string Detail { get; private set; }
    [field: SerializeField] public bool OnlyMoleWrong { get; private set; }
    [field: AllowNesting]
    [field: SerializeField, HideIf("OnlyMoleWrong")] public float WrongProbabilityNotMole { get; private set; }
    [field: SerializeField] public float WrongProbability { get; private set; }
    
}