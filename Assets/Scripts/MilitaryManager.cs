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
    [SerializeField] private string[] _passwordList;
    [SerializeField] private Military _mole;
    [SerializeField] private GameObject _card;
    [SerializeField] private GameObject _military;
    [SerializeField] private TextMeshProUGUI _passwordText;
    [SerializeField] private GameObject _dialogueSystem;
    [SerializeField] private string _startDialog;
    private DialogueRunner _dialogueRunner;
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
        SetMilitaryOrder();
        SetPassword();
        StartInterrogation();
    }

    private string GetPassword() => _selectedPassword;
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
    }
    private void SetPassword()
    {
        int passIndx = Random.Range(0, _passwordList.Length);

        _selectedPassword = _passwordList[passIndx];

        _passwordText.text = $" The password is\n{_selectedPassword}";
    }

    public void StartInterrogation()
    {
        _selectedMilitary = _militaryOrder?.Dequeue();
        SetMilitary();
        _cardManager.SetUpCard(_selectedMilitary);
        _militaryAnimator.SetTrigger("WalkIn");
       
    }
    public void HasWalkedIn()
    {
        _card.SetActive(true);
        _dialogueRunner.Stop();
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
        if (_selectedMilitary == _mole)
        {
            _militaryImage.sprite = _selectedMilitary.GetMoleSprite();
        }
        else
            _militaryImage.sprite = _selectedMilitary.Sprite[0];
    }
    
}
