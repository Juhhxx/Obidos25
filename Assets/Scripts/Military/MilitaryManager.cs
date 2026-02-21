using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Obidos25;
using TMPro;
using UnityEngine;

public class MilitaryManager : MonoBehaviourSingleton<MilitaryManager>
{
    [Header("Game Asset Library")]
    [Space(5f)]
    [SerializeField] private List<LocalizedScriptableObject<GameAssetLibrary>> _assetLibrarys;

    private GameAssetLibrary _assetLibrary;
    public GameAssetLibrary AssetLibrary => _assetLibrary;

    // Military
    private List<Military> MilitaryCharList => _assetLibrary.MilitaryCharacters;

    [Space(10f)]
    [Header("Military")]
    [Space(5f)]
    [SerializeField, ReadOnly] private List<Military> _militaryList;
    private Queue<Military> _militaryOrder;

    [Space(10f)]
    [Header("Moles")]
    [Space(5f)]
    [SerializeField, ReadOnly] private List<Military> _moles;
    public List<Military> Moles => _moles;

    private Military _selectedMilitary;
    public Military SelectedMilitary => _selectedMilitary;

    private MilitaryControl _militaryControl;
    private SpriteRenderer _militarySR;
    private Animator _militaryAnimator;

    // Passwords
    private PasswordCalendar PasswordsInfo => _assetLibrary.PasswordsInfo;

    [Space(10f)]
    [Header("Passwords")]
    [Space(5f)]
    [SerializeField][ReadOnly] private WeekDay _weekDay;
    public WeekDay WeekDay => _weekDay;

    private Password _selectedPassword;
    public Password SelectedPassword => _selectedPassword;

    // Parking Spots
    private List<ParkingSpot> ParkingSpots => _assetLibrary.ParkingSpots;

    [Space(10f)]
    [Header("Tickets")]
    [Space(5f)]
    [SerializeField] private Transform _greenTicketSpawn;
    [SerializeField] private Transform _redTicketSpawn;

    [SerializeField] private GameObject _greenTicketPrefab;
    [SerializeField] private GameObject _redTicketPrefab;

    [SerializeField] private GameObject _suspicionIndicator;


    private GameObject _greenTicket;
    private GameObject _redTicket;

    [Space(10f)]
    [Header("Game Objects")]
    [Space(5f)]
    [SerializeField] private Transform _giveItem;
    [SerializeField] private Transform _middlePoint;
    [SerializeField] private GameObject _idCard;
    [SerializeField] private GameObject _badgeBooklet;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _parkingMap;
    [SerializeField] private GameObject _passwordNotepad;
    [SerializeField] private GameObject _codenamesPaper;

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
    [SerializeField] private AnswerManager _answerManager;

    [Space(10f)]
    [Header("Win Check")]
    [Space(5f)]
    [SerializeField] private WinCheck _winCheck;

    [Space(10f)]
    [Header("Tutorial")]
    [Space(5f)]
    [SerializeField] private TutorialManager _tutorialManager;

    [Space(10f)]
    [Header("Music & Audio")]
    [Space(5f)]
    [SerializeField] private PlaySound _backgorundSoundPlayer;
    [SerializeField] private PlaySound _giveCardSoundPlayer;

    [Space(10f)]
    [Header("Cutscene")]
    [Space(5f)]

    [SerializeField] private List<LocalizedScriptableObject<Cutscene>> _contextCutscenes;
    private Cutscene _contextCutscene;

    [Space(10f)]
    [Header("Cheats")]
    [Space(5f)]
    [SerializeField] private bool _allowCheats;

    private CardManager _idCardManager;

    private void Awake()
    {
        base.SingletonCheck(this, false);

        _militaryControl = _military.GetComponent<MilitaryControl>();
        _militarySR = _military.GetComponentInChildren<SpriteRenderer>();
        _militaryAnimator = _military.GetComponent<Animator>();
        _idCardManager = _idCardBuilder.GetComponent<CardManager>();
    }

    private void Start()
    {
        _assetLibrary = LocalizedAssets.GetLocalization<LocalizedScriptableObject<GameAssetLibrary>>(_assetLibrarys, gameObject)?.ScriptableObject;
        _contextCutscene = LocalizedAssets.GetLocalization<LocalizedScriptableObject<Cutscene>>(_contextCutscenes, gameObject)?.ScriptableObject;

        CutsceneManager.Instance.PlayCutscene(_contextCutscene, () =>
        {
           _tutorialManager.StartDialogue();
           _backgorundSoundPlayer.SoundPlay(); 
        });

        StartGame();
    }

    public void StartGame()
    {
        _militaryList = new List<Military>();
        _militaryOrder = new Queue<Military>();

        _military.SetActive(false);
        _idCard.SetActive(false);
        _badgeBooklet.SetActive(false);
        _map.SetActive(false);
        _parkingMap.SetActive(false);
        _passwordNotepad.SetActive(false);
        _codenamesPaper.SetActive(false);

        SetPassword();

        SetMilitaryOrder();
        AssignParkingSpaces();
    }
    
    // Password
    private void SetPassword()
    {
        CalendarDay day = PasswordsInfo.ChooseWeekDay();

        _calendar.sprite = day.CalendarSprite;
        _weekDay = day.WeekDay;
    }

    // Parking Spots
    private void AssignParkingSpaces()
    {
        _parkingText.text = "";
        List<string> parkingSpaceTexts = new List<string>();
        List<ParkingSpot> parkingSpots = new List<ParkingSpot>(ParkingSpots);

        Debug.LogWarning("ASSIGNING PARKING SPACES", this);
        foreach (Military m in _militaryList)
        {
            int rnd = Random.Range(0, parkingSpots.Count);

            ParkingSpot ps = parkingSpots[rnd];

            m.SetParking(ps);

            parkingSpots.Remove(ps);

            parkingSpaceTexts.Add($"{ps.CarPlate} -> {m.ID}\n");
        }

        parkingSpaceTexts.Sort();

        foreach (string s in parkingSpaceTexts)
        {
            _parkingText.text += s;
            Debug.LogWarning(s, this);
        }

        _parkingMapBuilder?.BuildFileSprite();
    }

    // Tickets
    public void GenerateTickets()
    {
        CreateTickets(_greenTicketPrefab, _greenTicketSpawn, _greenTicket);
        CreateTickets(_redTicketPrefab, _redTicketSpawn, _redTicket);
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
        _giveCardSoundPlayer?.SoundPlay();

        if (type == TicketTypes.Red)
        {
            _suspicionIndicator.SetActive(true);
        }
        else if (type == TicketTypes.Green)
        {
            CreateTickets(_greenTicketPrefab, _greenTicketSpawn, _greenTicket);
            WalkingOut();
        }
    }
    public void GiveRedTicket()
    {
        if (_suspicionLevel == 0) return;

        _selectedMilitary?.Mark(_suspicionLevel);
        CreateTickets(_redTicketPrefab, _redTicketSpawn, _redTicket);

        _suspicionLevel = 0;
        _suspicionIndicator.SetActive(false);
        WalkingOut();
    }    

    private int _suspicionLevel;
    public void SetSuspicion(int level) => _suspicionLevel = level;

    // Items
    public void ShowIDCard()
    {
        _idCardBuilder?.BuildFileSprite();
        _idCard.SetActive(true);
    }

    public void GiveBadgeBooklet() => GiveItem(_badgeBooklet);
    public void GiveMap() => GiveItem(_map);
    public void GiveParkingMap() => GiveItem(_parkingMap);
    public void GivePasswordNotepad() => GiveItem(_passwordNotepad);
    public void GiveCodenames() => GiveItem(_codenamesPaper);

    public void ToggleIDCard(bool onOff) => _idCard.SetActive(onOff);
    public void ToggleBadgeBooklet(bool onOff) => _badgeBooklet.SetActive(onOff);
    public void ToggleMap(bool onOff) => _map.SetActive(onOff);
    public void ToggleParkingMap(bool onOff) => _parkingMap.SetActive(onOff);
    public void TogglePasswordNotepad(bool onOff) => _passwordNotepad.SetActive(onOff);
    public void ToggleCodenames(bool onOff) => _codenamesPaper.SetActive(onOff);

    private void GiveItem(GameObject item)
    {
        item.SetActive(true);

        Vector3 pos = _giveItem.position;
        pos.z = item.transform.position.z;

        item.transform.position = pos;

        item.GetComponent<CardItem>().ToggleCardItemSprite(true, false);
        item.GetComponent<Draggabble>().OnInteractBegin();
        item.GetComponent<Draggabble>().OnInteractEnd();
    }

    [Button(enabledMode: EButtonEnableMode.Always)]
    public void ShowEverythig()
    {
        var objs = new List<GameObject> { _badgeBooklet, _map, _parkingMap, _passwordNotepad, _codenamesPaper};
        var tmp = new List<GameObject>();
        var pos = new List<Vector3>();

        foreach (GameObject go in objs)
        {
            if (go.GetComponent<CardItem>().IsItem) continue;

            tmp.Add(go);
            pos.Add(go.transform.position);

            Vector3 dir = go.transform.position - _middlePoint.position;

            dir.z = 0;
            dir = Vector3.Normalize(dir);

            go.transform.position = go.transform.position + (dir * 25);
        }

        ToggleBadgeBooklet(true);
        ToggleMap(true);
        ToggleParkingMap(true);
        TogglePasswordNotepad(true);
        ToggleCodenames(true);

        for (int i = 0; i < tmp.Count; i++)
        {
            StartCoroutine(Move(tmp[i], pos[i]));
        }
    }

    private IEnumerator Move(GameObject document, Vector3 targetPos)
{
    Vector3 startPos = document.transform.position;
    float t = 0f;

    while (t < 1f)
    {
        t += 2f * Time.deltaTime;

        float x = Mathf.Lerp(startPos.x, targetPos.x, t);
        float y = Mathf.Lerp(startPos.y, targetPos.y, t);

        document.transform.position = new Vector3(x, y, startPos.z);

        yield return null;
    }

    document.transform.position = new Vector3(
        targetPos.x,
        targetPos.y,
        startPos.z
    );
}

    // Military and Moles
    private void SetMilitaryOrder()
    {
        for (int i = 0; i < MilitaryCharList.Count; i++) _militaryList.Add(MilitaryCharList[i].Instantiate());

        _winCheck.SetMilitary(_militaryList);

        SetMoles();

        _militaryList.Shuffle();

        _militaryOrder = new Queue<Military>(_militaryList);
    }
    public void SetMilitary(Military selectedMilitary)
    {
        Debug.Log(_militarySR);

        _military.SetActive(true);

        if (selectedMilitary.WrongAnswers["sprite"])
        {
            _militarySR.sprite = selectedMilitary.GetMoleSprite();
        }
        else
            _militarySR.sprite = selectedMilitary.Sprite[0];

        if (selectedMilitary.WrongAnswers["eye_color"])
        {
            _militaryControl.ChangeEyeColor(_assetLibrary.GetWrongEyeColor(selectedMilitary.EyeColor).Color);
        }
        else
            _militaryControl.ChangeEyeColor(selectedMilitary.EyeColor.Color);

        SetBadges(selectedMilitary);
        SetIdCard(selectedMilitary);
    }
    private void SetBadges(Military selectedMilitary)
    {
        if (selectedMilitary.WrongAnswers["rank_badge"])
        {
            _rank.sprite = _assetLibrary.GetWrongBadge(selectedMilitary.Rank, true).Badge;
        }
        else
        {
            _rank.sprite = selectedMilitary.Rank.Badge;
        }

        if (selectedMilitary.WrongAnswers["division_badge"])
        {
            _division.sprite = _assetLibrary.GetWrongBadge(selectedMilitary.Division, false).Badge;
        }
        else
        {
            _division.sprite = selectedMilitary.Division.Badge;
        }

        _rank.gameObject.SetActive(true);
        _division.gameObject.SetActive(true);

        if (_selectedMilitary == null) return;

        if (Random.Range(0f, 1f) <= 0.2f)
        {
            if (Random.Range(0,2) == 0) _rank.gameObject.SetActive(false);
            else _rank.gameObject.SetActive(false);

            if (Random.Range(0f, 1f) <= 0.05f)
            {
                _rank.gameObject.SetActive(false);
                _division.gameObject.SetActive(false);
            }
        }
        
    }
    private void SetIdCard(Military selectedMilitary)
    {
        _idCardManager.SetUpCard(selectedMilitary);
        _idCard.GetComponent<Draggabble>().ResetPosition();
    }

    // Moles
    private void SetMoles()
    {
        int moleNumber = Random.Range(1,4);

        Debug.LogWarning($"{moleNumber} MOLES");

        for (int i = 0; i < moleNumber; i++)
        {
            _moles.Add(ChooseMole());
        }

        _winCheck.SetMoles(moleNumber, _moles);
    }
    private Military ChooseMole()
    {
        int moleChooser = Random.Range(0, _militaryList.Count);

        if (_militaryList[moleChooser].IsMole)
        {
            return ChooseMole();
        }
        
        Military m = _militaryList[moleChooser];

        m.SetMole();

        return m;
    }

    // Gameplay
    public void StartInterrogation()
    {
        if (_militaryOrder.Count == 0)
        {
            EndInterrogation();
            return;
        }

        _selectedPassword = PasswordsInfo.GetPassword();
        _selectedMilitary = _militaryOrder?.Dequeue();
        _answerManager.ResetAnswers();

        SetMilitary(_selectedMilitary);

        _answerManager.StopDialogue();
        _militaryAnimator.SetTrigger("WalkIn");
    }

    private void EndInterrogation()
    {
        _winCheck.SetPortaits();
        _winCheck.StartFinal();
    }

    public void Update()
    {
        if (_allowCheats && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L)) EndInterrogation();
    }

    public void HasWalkedIn()
    {
        if (_selectedMilitary == null) return;

        _answerManager.StartDialogue();
    }
    public void WalkingOut()
    {
        _idCard.SetActive(false);
        _militaryAnimator.SetTrigger("WalkOut");
    }
}