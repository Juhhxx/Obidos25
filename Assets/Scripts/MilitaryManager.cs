using System.Collections.Generic;
using Obidos25;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class MilitaryManager : MonoBehaviour
{
    [SerializeField] private List<Military> _militaryList;
    private Queue<Military> _militaryOrder;
    [SerializeField] private List<string> _passwordList;
    [SerializeField] private Military _mole;
    [SerializeField] private GameObject _card;
    [SerializeField] private GameObject _military;
    [SerializeField] private BadgeManager _rank;
    [SerializeField] private BadgeManager _division;
    [SerializeField] private TextMeshProUGUI _passwordText;
    [SerializeField] private GameObject _dialogueSystem;
    [SerializeField] private string _startDialog;
    [SerializeField] private WinCheck _winCheck;
    private DialogueRunner _dialogueRunner;
    private InMemoryVariableStorage _dialogueVariables;
    private Military _selectedMilitary;
    private string _selectedPassword;
    private Image _militaryImage;
    private Animator _militaryAnimator;
    private CardManager _cardManager;
 
    private void Awake()
    {
        _militaryImage = _military.GetComponentInChildren<Image>();
        _militaryAnimator = _military.GetComponent<Animator>();
        _cardManager = _card.GetComponent<CardManager>();
        _dialogueRunner = _dialogueSystem.GetComponent<DialogueRunner>();
        _dialogueVariables = _dialogueSystem.GetComponent<InMemoryVariableStorage>();

        _dialogueRunner.AddFunction("get_military_name",GetName);
        _dialogueRunner.AddFunction("get_password_dialog",GetPassword);
        _dialogueRunner.AddFunction("get_location_dialog",GetLocation);
        _dialogueRunner.AddFunction("get_park_dialog",GetParking);
        _dialogueRunner.AddFunction("get_division_dialog",GetDivision);
        _dialogueRunner.AddFunction("get_rank_dialog",GetRank);
        _dialogueRunner.AddFunction("get_codename_dialog",GetCodeName);
    }
    private void Start()
    {
        _card.SetActive(false);
        SetMilitaryOrder();
        SetPassword();
        StartInterrogation();
    }

    private string GetPassword() 
    {
        if (_selectedMilitary == _mole && MoleChance(80))
        {
            int passIndx = Random.Range(0, _passwordList.Count);

            return _passwordList[passIndx];
        }
        else
            return _selectedPassword;
    }
    private string GetName() => _selectedMilitary.Name;
    private string GetCodeName() => _selectedMilitary.CodeName;
    private string GetDivision() => _selectedMilitary.Division.ToString();
    private string GetRank() => _selectedMilitary.Rank;
    private string GetParking() => _selectedMilitary.ParkingSpot;
    private string GetLocation() => _selectedMilitary.Location;


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
        _winCheck.Mole = mole;
    }
    private void SetPassword()
    {
        int passIndx = Random.Range(0, _passwordList.Count);

        _selectedPassword = _passwordList[passIndx];

        _passwordList.Remove(_selectedPassword);

        _passwordText.text = $" The password is\n{_selectedPassword}";
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
        _dialogueRunner.Stop();
        _cardManager.SetUpCard(_selectedMilitary);
        _militaryAnimator.SetTrigger("WalkIn");
       
    }
    public void HasWalkedIn()
    {
        _card.SetActive(true);
        _dialogueRunner.StartDialogue(_startDialog);
    }
    public void WalkingOut()
    {
        _card.SetActive(false);
        _militaryAnimator.SetTrigger("WalkOut");
    }
    private void SetMilitary()
    {
        Debug.Log(_militaryImage);
        if (_selectedMilitary == _mole && MoleChance(10))
        {
            _militaryImage.sprite = _selectedMilitary.GetMoleSprite();
        }
        else
            _militaryImage.sprite = _selectedMilitary.Sprite[0];
        
        SetBadges(_rank, _selectedMilitary.Rank);
        SetBadges(_division, _selectedMilitary.Division.ToString());
    }
    private void SetBadges(BadgeManager badge, string militaryBadge)
    {
        if (_selectedMilitary == _mole && MoleChance(40))
        {
            badge.SetBadge(militaryBadge, true);
        }
        else
        {
            badge.SetBadge(militaryBadge, false);
        }
    }
    private bool MoleChance(int chance)
    {
        int moleChance = Random.Range(0,100);

        return moleChance <= chance;
    }
    
}
