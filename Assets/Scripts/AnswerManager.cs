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
            return Password.GetPasswordAnswerWrong(WeekDay);
        }
        else
            return Password.GetPasswordAnswer(WeekDay);
    }
    private string GetName() => SelectedMilitary.Name;
    private string GetCodeName()
    {
        if (SelectedMilitary.WrongAnswers["codename"])
        {
            return AssetLibrary.GetWrongCodeName(SelectedMilitary);
        }
        else
            return SelectedMilitary.CodeName;
    }
    private string GetParking()
    {
        if (SelectedMilitary.WrongAnswers["parking"])
        {
            return AssetLibrary.GetWrongParkingSpot(SelectedMilitary.ParkingSpot).Spot;
        }
        else
            return SelectedMilitary.ParkingSpot.Spot;
    }
    private string GetLocation()
    {
        if (SelectedMilitary.WrongAnswers["location"])
        {
            return AssetLibrary.GetWrongLocation(SelectedMilitary.Location).Name;
        }
        else
            return SelectedMilitary.Location.Name;
    }

}
