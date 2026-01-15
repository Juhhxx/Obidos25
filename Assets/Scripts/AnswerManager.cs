using System;
using System.Collections.Generic;
using Obidos25;
using UnityEngine;
using Yarn.Unity;

public class AnswerManager : MonoBehaviour
{
    [Space(10f)]
    [Header("Dialogue")]
    [Space(5f)]
    [SerializeField] private GameObject _dialogueSystem;
    [SerializeField] private string _startDialog;
    private DialogueRunner _dialogueRunner;
    private InMemoryVariableStorage _dialogueVariables;

    private GameAssetLibrary AssetLibrary => MilitaryManager.Instance.AssetLibrary;
    private Military SelectedMilitary => MilitaryManager.Instance.SelectedMilitary;
    private Password Password => MilitaryManager.Instance.SelectedPassword;
    private WeekDay WeekDay => MilitaryManager.Instance.WeekDay;

    [SerializeField] private List<string> _nameVariations;
    [SerializeField] private List<string> _codeNameVariations;
    [SerializeField] private List<string> _parkingVariations;
    [SerializeField] private List<string> _locationVariations;

    private string _passwordAnswer;
    private string _codeNameAnswer;
    private string _parkingAnswer;
    private string _locationAnswer;

    public void ResetAnswers()
    {
        _passwordAnswer = "";
        _codeNameAnswer = "";
        _parkingAnswer = "";
        _locationAnswer = "";
    }

    public void Awake()
    {
        _dialogueRunner = _dialogueSystem.GetComponent<DialogueRunner>();
        _dialogueVariables = _dialogueSystem.GetComponent<InMemoryVariableStorage>();

        _dialogueRunner.AddFunction("give_documents",  GiveDocuments);
        _dialogueRunner.AddFunction("get_military_name", GetName);
        _dialogueRunner.AddFunction("get_password_question", GetPassword);
        _dialogueRunner.AddFunction("get_password_dialog", GetPasswordAnswer);
        _dialogueRunner.AddFunction("get_location_dialog", GetLocation);
        _dialogueRunner.AddFunction("get_park_dialog", GetParking);
        _dialogueRunner.AddFunction("get_codename_dialog", GetCodeName);
    }

    public void StartDialogue()
    {
        _dialogueRunner.StartDialogue(_startDialog);
    }

    public void StopDialogue()
    {
        _dialogueRunner.Stop();
        _dialogueRunner.Clear();
    }

    // Questions
    private string GiveDocuments()
    {
        MilitaryManager.Instance.ShowIDCard();
        return "";
    }
    private string GetPassword()
    {
        return Password.PasswordQuestion;
    }
    private string GetPasswordAnswer()
    {
        if (SelectedMilitary.WrongAnswers["password"])
        {
            string answer = _passwordAnswer == "" ? Password.GetPasswordAnswerWrong(WeekDay) : _passwordAnswer;

            _passwordAnswer = answer;

            return _passwordAnswer;
        }
        else
            return Password.GetPasswordAnswer(WeekDay);
    }
    private string GetName() => NameVariations(SelectedMilitary.Name);
    private string GetCodeName()
    {
        if (SelectedMilitary.WrongAnswers["codename"])
        {
            string answer = _codeNameAnswer == "" ? AssetLibrary.GetWrongCodeName(SelectedMilitary) : _codeNameAnswer;

            _codeNameAnswer = answer;

            return CodeNameVariations(_codeNameAnswer);
        }
        else
            return CodeNameVariations(SelectedMilitary.CodeName);
    }
    private string GetParking()
    {
        if (SelectedMilitary.WrongAnswers["parking"])
        {
            string answer = _parkingAnswer == "" ? 
            AssetLibrary.GetWrongParkingSpot(SelectedMilitary.ParkingSpot).Spot : _parkingAnswer;
            
            _parkingAnswer = answer;

            return ParkingVariations(_parkingAnswer);
        }
        else
            return ParkingVariations(SelectedMilitary.ParkingSpot.Spot);
    }
    private string GetLocation()
    {
        if (SelectedMilitary.WrongAnswers["location"])
        {
            string answer = _locationAnswer == "" ? 
            AssetLibrary.GetWrongLocation(SelectedMilitary.Location).Name : _locationAnswer;

            _locationAnswer = answer;

            return LocationVariations(_locationAnswer);
        }
        else
            return LocationVariations(SelectedMilitary.Location.Name);
    }

    private string NameVariations(string name)
    {
        int rnd = UnityEngine.Random.Range(0, _nameVariations.Count);

        return string.Format(_nameVariations[rnd], name);
    }

    private string CodeNameVariations(string codename)
    {
        int rnd = UnityEngine.Random.Range(0, _codeNameVariations.Count);

        return string.Format(_codeNameVariations[rnd], codename);
    }

    private string  ParkingVariations(string parking)
    {
        int rnd = UnityEngine.Random.Range(0, _parkingVariations.Count);

        return string.Format(_parkingVariations[rnd], parking);
    }

    private string LocationVariations(string location)
    {
        int rnd = UnityEngine.Random.Range(0, _locationVariations.Count);

        return string.Format(_locationVariations[rnd], location);
    }
}
