using System;
using System.Collections;
using Obidos25;
using UnityEngine;
using Yarn.Unity;

public class TutorialManager : MonoBehaviour
{
    [Header("Dialogue")]
    [Space(5f)]
    [SerializeField] private GameObject _dialogueSystem;
    [SerializeField] private string _tutorialDialog;
    private DialogueRunner _dialogueRunner;
    private InMemoryVariableStorage _dialogueVariables;

    [Space(10f)]
    [Header("Dialogue")]
    [Space(5f)]
    [SerializeField] private Animator _anim;

    private Military General => MilitaryManager.Instance.AssetLibrary.General;

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
        _dialogueRunner.AddFunction("give_everything", GiveEverything);
        _dialogueRunner.AddFunction("tutorial_end", EndTutorial);
    }

    private void Start()
    {
        OnTutorialEnd += () => {
            MilitaryManager.Instance.ToggleIDCard(false);
            _anim.SetTrigger("WalkOut");
        };
    }

    public void StartDialogue()
    {
        MilitaryManager.Instance.SetMilitary(General);
        _anim.SetTrigger("WalkIn");

        StartCoroutine(WaitForAnimation(_anim, () => _dialogueRunner.StartDialogue(_tutorialDialog)));
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

    private IEnumerator WaitForAnimation(Animator anim, Action onEnd)
    {
        yield return new WaitUntil(() => !AnimatorIsPlaying(anim));

        onEnd.Invoke();
    }

    private bool AnimatorIsPlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private string GiveID()
    {
        MilitaryManager.Instance.ShowIDCard();
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
    private string GiveEverything()
    {
        MilitaryManager.Instance.GiveBadgeBooklet();
        MilitaryManager.Instance.GiveMap();
        MilitaryManager.Instance.GiveParkingMap();
        MilitaryManager.Instance.GivePasswordNotepad();
        MilitaryManager.Instance.GiveCodenames();
        MilitaryManager.Instance.GenerateTickets();
        return "";
    }
}
