using System.Collections.Generic;
using Obidos25;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class MilitaryManager : MonoBehaviour
{
    [SerializeField] private List<Military> _militaryList;
    private Queue<Military> _militaryOrder;
    [SerializeField] private Military _mole;
    [SerializeField] private CardManager _card;
    [SerializeField] private Image _militaryImage;
    [SerializeField] private GameObject _dialogueSystem;
    private DialogueRunner _dialogueRunner;
    private Military _selectedMilitary;
 
    private void Awake()
    {
        _dialogueRunner = _dialogueSystem.GetComponent<DialogueRunner>();

        _dialogueRunner.AddFunction("get_military_name",GetName);
    }
    private void Start()
    {
        SetMilitaryOrder();
        StartInterrogation();
    }

    private string GetName() => _selectedMilitary.Name;

    private void SetMilitaryOrder()
    {
        int moleChooser = Random.Range(0,_militaryList.Count);

        SetMole(_militaryList[moleChooser]);

        _militaryList.Shuffle();

        _militaryOrder = new Queue<Military>(_militaryList);
    }
    private void SetMole(Military mole)
    {
        _mole = mole;
    }

    public void StartInterrogation()
    {
        _selectedMilitary = _militaryOrder?.Dequeue();
        SetMilitary();
        _card.SetUpCard(_selectedMilitary);
        _dialogueRunner.Stop();
        _dialogueRunner.StartDialogue("Interrogation");

    }
    private void SetMilitary()
    {
        if (_selectedMilitary == _mole)
        {
            _militaryImage.sprite = _selectedMilitary.GetMoleSprite();
        }
        else
            _militaryImage.sprite = _selectedMilitary.Sprite[0];
    }
    
}
