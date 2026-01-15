using System;
using UnityEngine;
using Yarn.Unity;

public class TutorialManager : MonoBehaviour
{
    [Space(10f)]
    [Header("Dialogue")]
    [Space(5f)]
    [SerializeField] private GameObject _dialogueSystem;
    [SerializeField] private string _tutorialDialog;
    private DialogueRunner _dialogueRunner;
    private InMemoryVariableStorage _dialogueVariables;

    public event Action OnTutorialEnd;

    private void Awake()
    {
        _dialogueRunner = _dialogueSystem.GetComponent<DialogueRunner>();
        _dialogueVariables = _dialogueSystem.GetComponent<InMemoryVariableStorage>();

        _dialogueRunner.AddFunction("give_id",  GiveID);
        _dialogueRunner.AddFunction("give_badge_booklet", GiveBadgeBooklet);
        _dialogueRunner.AddFunction("give_map", GiveMap);
        _dialogueRunner.AddFunction("give_parking_map", GiveParkingMap);
        _dialogueRunner.AddFunction("give_password_notepad", GivePasswordNotepad);
        _dialogueRunner.AddFunction("give_codenames", GiveCodenames);
        _dialogueRunner.AddFunction("give_tickets", GiveTickets);
        _dialogueRunner.AddFunction("tutorial_end", EndTutorial);
    }

    private void Start()
    {
        OnTutorialEnd += MilitaryManager.Instance.StartInterrogation;
    }

    public void StartDialogue()
    {
        _dialogueRunner.StartDialogue(_tutorialDialog);
    }

    public void StopDialogue()
    {
        _dialogueRunner.Stop();
        _dialogueRunner.Clear();
    }

    private string EndTutorial()
    {
        OnTutorialEnd?.Invoke();
        return "";
    }

    private string GiveID()
    {
        return "";
    }

    private string GiveBadgeBooklet()
    {
        MilitaryManager.Instance.GiveBadgeBooklet();
        return "";
    }
    private string GiveMap()
    {
        MilitaryManager.Instance.GiveMap();
        return "";
    }
    private string GiveParkingMap()
    {
        MilitaryManager.Instance.GiveParkingMap();
        return "";
    }
    private string GivePasswordNotepad()
    {
        MilitaryManager.Instance.GivePasswordNotepad();
        return "";
    }
    private string GiveCodenames()
    {
        MilitaryManager.Instance.GiveCodenames();
        return "";
    }
    private string GiveTickets()
    {
        MilitaryManager.Instance.GenerateTickets();
        return "";
    }
}
