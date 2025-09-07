using System.Collections.Generic;
using Obidos25;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class MilitaryManager : MonoBehaviourSingleton<MilitaryManager>
{
    [Header("Military Characters")]
    [Space(5f)]
    [SerializeField] private List<Military> _militaryList;
    private Queue<Military> _militaryOrder;
    [SerializeField] private List<Military> _moles;

    private Military _selectedMilitary;
    private SpriteRenderer _militarySR;
    private Animator _militaryAnimator;

    [Space(10f)]
    [Header("Passwords")]
    [Space(5f)]
    [SerializeField] private List<Password> _passwordList;
    
    private Password _selectedPassword;

    [System.Serializable]
    public struct Password
    {
        [field:SerializeField] public string PasswordKey { get; private set; }

        [field:SerializeField] public string PasswordCorrectAnswer { get; private set; }
        [field:SerializeField] public string PasswordWrongAnswer { get; private set; }
    }

    [Space(10f)]
    [Header("Game Objects")]
    [Space(5f)]
    [SerializeField] private GameObject _idCard;
    [SerializeField] private DynamicFileBuilder _idCardBuilder;
    [SerializeField] private TextMeshProUGUI _passwordText;
    [SerializeField] private DynamicFileBuilder _passwordNoteBuilder;
    [SerializeField] private GameObject _ticketStacks;

    [Space(10f)]
    [Header("Military Object")]
    [Space(5f)]
    [SerializeField] private GameObject _military;
    [SerializeField] private SpriteRenderer _rank;
    [SerializeField] private SpriteRenderer _division;

    [Space(10f)]
    [Header("Dialogue")]
    [Space(5f)]
    [SerializeField] private GameObject _dialogueSystem;
    [SerializeField] private string _startDialog;

    [Space(10f)]
    [Header("Win Check")]
    [Space(5f)]
    [SerializeField] private WinCheck _winCheck;

    private DialogueRunner _dialogueRunner;
    private InMemoryVariableStorage _dialogueVariables;
    
    private CardManager _idCardManager;

    [Space(10f)]
    [Header("Events")]
    [Space(5f)]
    public UnityEvent OnUpdateAssets;
 
    private void Awake()
    {
        SingletonCheck(this);

        _militarySR = _military.GetComponentInChildren<SpriteRenderer>();
        _militaryAnimator = _military.GetComponent<Animator>();
        _dialogueRunner = _dialogueSystem.GetComponent<DialogueRunner>();
        _dialogueVariables = _dialogueSystem.GetComponent<InMemoryVariableStorage>();
        _idCardManager = _idCardBuilder.GetComponent<CardManager>();

        _dialogueRunner.AddFunction("get_military_name", GetName);
        _dialogueRunner.AddFunction("get_password_question", GetPassword);
        _dialogueRunner.AddFunction("get_password_dialog", GetPasswordAnswer);
        _dialogueRunner.AddFunction("get_location_dialog", GetLocation);
        _dialogueRunner.AddFunction("get_park_dialog", GetParking);
        _dialogueRunner.AddFunction("get_division_dialog", GetDivision);
        _dialogueRunner.AddFunction("get_rank_dialog", GetRank);
        _dialogueRunner.AddFunction("get_codename_dialog", GetCodeName);
    }
    private void Start()
    {
        _idCard.SetActive(false);

        SetMilitaryOrder();
        SetPassword();
        StartInterrogation();
        _passwordNoteBuilder?.BuildFileSprite();
    }

    // Questions
    private string GetPassword()
    {
        int rnd = Random.Range(0, _passwordList.Count);

        _selectedPassword = _passwordList[rnd];

        return _selectedPassword.PasswordKey;
    }
    private string GetPasswordAnswer()
    {
        if (_moles.Contains(_selectedMilitary) && MoleChance(10))
        {
            return _selectedPassword.PasswordWrongAnswer;
        }
        else
            return _selectedPassword.PasswordCorrectAnswer;
    }
    private string GetName() => _selectedMilitary.Name;
    private string GetCodeName()
    {
        if (_moles.Contains(_selectedMilitary) && MoleChance(40))
        {
            int milIdx = Random.Range(0, _militaryList.Count);

            return _militaryList[milIdx].CodeName;
        }
        else
            return _selectedMilitary.CodeName;
    }
    private string GetDivision() => _selectedMilitary.Division.DivisionName;
    private string GetRank() => _selectedMilitary.Rank.RankName;
    private string GetParking()
    {
        if (_moles.Contains(_selectedMilitary) && MoleChance(40))
        {
            int milIdx = Random.Range(0, _militaryList.Count);

            return _militaryList[milIdx].ParkingSpot;
        }
        else
            return _selectedMilitary.ParkingSpot;
    }
    private string GetLocation()
    {
        if (_moles.Contains(_selectedMilitary) && MoleChance(40))
        {
            int milIdx = Random.Range(0, _militaryList.Count);

            return _militaryList[milIdx].Location;
        }
        else
            return _selectedMilitary.Location;
    }

    // Military and Moles
    private void SetMilitaryOrder()
    {
        SetMoles();
        
        _militaryList.Shuffle();

        _militaryOrder = new Queue<Military>(_militaryList);

        foreach (Military m in _moles) _militaryList.Remove(m);
    }
    public void GiveTicket(TicketTypes type)
    {
        if (type == TicketTypes.Red) _selectedMilitary?.Mark();

        WalkingOut();
    }
    private void SetMilitary()
    {
        Debug.Log(_militarySR);

        if (_moles.Contains(_selectedMilitary) && MoleChance(50))
        {
            _militarySR.sprite = _selectedMilitary.GetMoleSprite();
        }
        else
            _militarySR.sprite = _selectedMilitary.Sprite[0];

        _rank.sprite = _selectedMilitary.Rank.RankBadge;
        _division.sprite = _selectedMilitary.Division.DivisionBadge;
        
        // SetBadges(_rank, _selectedMilitary.Rank.RankName);
        // SetBadges(_division, _selectedMilitary.Division.DivisionName);
    }
    private void SetMoles()
    {
        int moleNumber = Random.Range(1,4);

        Debug.LogWarning($"{moleNumber} MOLES");

        for (int i = 0; i < moleNumber; i++)
        {
            _moles.Add(ChooseMole());
        }
    }
    private Military ChooseMole()
    {
        int moleChooser = Random.Range(0, _militaryList.Count);

        if (_moles.Contains(_militaryList[moleChooser]))
        {
            return ChooseMole();
        }
        else return _militaryList[moleChooser];
    }
    private bool MoleChance(int chance)
    {
        int moleChance = Random.Range(0, 100);

        return moleChance <= chance;
    }
    
    private void SetPassword()
    {
        string passwords = "";

        foreach (Password p in _passwordList)
        {
            passwords += p.PasswordKey;

            passwords += " > ";

            passwords += p.PasswordCorrectAnswer + "\n";
        }

        _passwordText.text = passwords;
    }

    public void StartInterrogation()
    {
        if (_militaryOrder.Count == 0)
        {
            _winCheck.StartFinal();
            return;
        }

        _selectedMilitary = _militaryOrder?.Dequeue();

        SetMilitary();
        _idCardManager.SetUpCard(_selectedMilitary);
        _idCard.GetComponent<Draggabble>().ResetPosition();

        TicketStack[] ts = _ticketStacks.GetComponentsInChildren<TicketStack>();

        foreach (TicketStack t in ts) t.Reset();

        _dialogueRunner.Stop();
        _militaryAnimator.SetTrigger("WalkIn");
    }
    public void HasWalkedIn()
    {
        _idCardBuilder?.BuildFileSprite();
        _idCard.SetActive(true);
        _dialogueRunner.StartDialogue(_startDialog);
    }
    public void WalkingOut()
    {
        _idCard.SetActive(false);
        _militaryAnimator.SetTrigger("WalkOut");
    }
    
    // private void SetBadges(GameObject badge, string militaryBadge)
    // {
    //     if (_moles.Contains(_selectedMilitary) && MoleChance(80))
    //     {
    //         badge.SetBadge(militaryBadge, true);
    //     }
    //     else
    //     {
    //         badge.SetBadge(militaryBadge, false);
    //     }
    // }
    
    
}
