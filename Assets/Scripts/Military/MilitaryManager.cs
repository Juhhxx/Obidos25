using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Obidos25;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class MilitaryManager : MonoBehaviourSingleton<MilitaryManager>
{
    [Header("Game Asset Library")]
    [Space(5f)]
    [SerializeField, Expandable] private GameAssetLibrary _assetLibrary;


    // Military
    private List<Military> MilitaryCharList => _assetLibrary.MilitaryCharacters;

    [Space(10f)]
    [Header("Military")]
    [Space(5f)]
    [SerializeField] private List<Military> _militaryList = new List<Military>();
    private Queue<Military> _militaryOrder = new Queue<Military>();

    [Space(10f)]
    [Header("Moles")]
    [Space(5f)]
    [SerializeField] private List<Military> _moles;
    public List<Military> Moles => _moles;

    private Military _selectedMilitary;
    private SpriteRenderer _militarySR;
    private Animator _militaryAnimator;

    // Passwords
    private PasswordCalendar PasswordsInfo => _assetLibrary.PasswordsInfo;

    [Space(10f)]
    [Header("Passwords")]
    [Space(5f)]
    [SerializeField][ReadOnly] private WeekDay _weekDay;
    private Password _selectedPassword;

    // Parking Spots
    private List<ParkingSpot> ParkingSpots => _assetLibrary.ParkingSpots;

    [Space(10f)]
    [Header("Tickets")]
    [Space(5f)]
    [SerializeField] private Transform _greenTicketSpawn;
    [SerializeField] private Transform _redTicketSpawn;

    [SerializeField] private GameObject _greenTicketPrefab;
    [SerializeField] private GameObject _redTicketPrefab;

    private GameObject _greenTicket;
    private GameObject _redTicket;

    [Space(10f)]
    [Header("Game Objects")]
    [Space(5f)]
    [SerializeField] private GameObject _idCard;
    [SerializeField] private DynamicFileBuilder _idCardBuilder;
    [SerializeField] private SpriteRenderer _calendar;
    [SerializeField] private TextMeshProUGUI _parkingText;
    [SerializeField] private DynamicFileBuilder _parkingMapBuilder;
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

    private void Awake()
    {
        SingletonCheck(this);

        _militarySR = _military.GetComponentInChildren<SpriteRenderer>();
        _militaryAnimator = _military.GetComponent<Animator>();
        _dialogueRunner = _dialogueSystem.GetComponent<DialogueRunner>();
        _dialogueVariables = _dialogueSystem.GetComponent<InMemoryVariableStorage>();
        _idCardManager = _idCardBuilder.GetComponent<CardManager>();

        _dialogueRunner.AddFunction("give_documents",  GiveDocuments);
        _dialogueRunner.AddFunction("get_military_name", GetName);
        _dialogueRunner.AddFunction("get_password_question", GetPassword);
        _dialogueRunner.AddFunction("get_password_dialog", GetPasswordAnswer);
        _dialogueRunner.AddFunction("get_location_dialog", GetLocation);
        _dialogueRunner.AddFunction("get_park_dialog", GetParking);
        _dialogueRunner.AddFunction("get_codename_dialog", GetCodeName);
    }
    private void Start()
    {
        _idCard.SetActive(false);

        CreateTickets(_greenTicketPrefab, _greenTicketSpawn, _greenTicket);
        CreateTickets(_redTicketPrefab, _redTicketSpawn, _redTicket);

        SetPassword();

        AssignParkingSpaces();

        SetMilitaryOrder();
        StartInterrogation();
    }

    private void SetPassword()
    {
        CalendarDay day = PasswordsInfo.ChooseWeekDay();

        _calendar.sprite = day.CalendarSprite;
        _weekDay = day.WeekDay;
    }

    private void AssignParkingSpaces()
    {
        _parkingText.text = "";
        List<string> parkingSpaceTexts = new List<string>();

        foreach (Military m in _militaryList)
        {
            int rnd = Random.Range(0, ParkingSpots.Count);

            ParkingSpot ps = ParkingSpots[rnd];

            m.SetParking(ps);

            ParkingSpots.Remove(ps);

            parkingSpaceTexts.Add($"{ps.CarPlate} -> {m.ID}\n");
        }

        parkingSpaceTexts.Sort();

        foreach (string s in parkingSpaceTexts) _parkingText.text += s;

        _parkingMapBuilder?.BuildFileSprite();
    }

    private void CreateTickets(GameObject prefab, Transform spawn, GameObject ticket)
    {
        ticket = Instantiate(prefab, spawn.position + new Vector3(10, 0, 0), Quaternion.identity);

        StartCoroutine(MoveTicket(ticket, spawn.position));
    }

    private IEnumerator MoveTicket(GameObject ticket, Vector3 pos)
    {
        float  initialPos = ticket.transform.position.x;
        float newPos = initialPos;
        float i = 0;

        while (newPos != pos.x)
        {
            newPos = Mathf.Lerp(initialPos, pos.x, i);

            Vector3 p = ticket.transform.localPosition;

            p.x = newPos;

            ticket.transform.position = p;

            i += 2 * Time.deltaTime;

            yield return null;
        }
    }

    public void GiveTicket(TicketTypes type)
    {
        if (type == TicketTypes.Red)
        {
            _selectedMilitary?.Mark();
            CreateTickets(_redTicketPrefab, _redTicketSpawn, _redTicket);

        }
        else if (type == TicketTypes.Green) CreateTickets(_greenTicketPrefab, _greenTicketSpawn, _greenTicket);

        WalkingOut();
    }

    // Questions
    private string GiveDocuments()
    {
        _idCardBuilder?.BuildFileSprite();
        _idCard.SetActive(true);
        return "";
    }
    private string GetPassword()
    {
        return _selectedPassword.PasswordQuestion;
    }
    private string GetPasswordAnswer()
    {
        if (_moles.Contains(_selectedMilitary) && MoleChance(50))
        {
            return _selectedPassword.GetPasswordAnswerWrong(_weekDay);
        }
        else
            return _selectedPassword.GetPasswordAnswer(_weekDay);
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
    private string GetParking()
    {
        if (_moles.Contains(_selectedMilitary) && MoleChance(40))
        {
            int milIdx = Random.Range(0, _militaryList.Count);

            return _militaryList[milIdx].ParkingSpot.Spot;
        }
        else
            return _selectedMilitary.ParkingSpot.Spot;
    }
    private string GetLocation()
    {
        if (_moles.Contains(_selectedMilitary) && MoleChance(40))
        {
            int milIdx = Random.Range(0, _militaryList.Count);

            return _militaryList[milIdx].Location.Name;
        }
        else
            return _selectedMilitary.Location.Name;
    }

    // Military and Moles
    private void SetMilitaryOrder()
    {
        for (int i = 0; i < MilitaryCharList.Count; i++) _militaryList.Add(MilitaryCharList[i].Instantiate());

        _winCheck.SetMilitary(_militaryList);

        SetMoles();

        _winCheck.SetMoles(_moles);

        _militaryList.Shuffle();

        _militaryOrder = new Queue<Military>(_militaryList);
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
        int moleChance = Random.Range(1, 101);

        return moleChance <= chance;
    }

    public void StartInterrogation()
    {
        if (_militaryOrder.Count == 0)
        {
            _winCheck.SetPortaits();
            _winCheck.StartFinal();
            return;
        }

        _selectedPassword = PasswordsInfo.GetPassword();
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
