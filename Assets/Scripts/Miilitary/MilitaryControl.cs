using UnityEngine;

public class MilitaryControl : MonoBehaviour
{
    [SerializeField] private MilitaryManager _militaryManager;

    public void CallStartInterrogation() => _militaryManager.StartInterrogation();
    public void CallHasWalkedIn() => _militaryManager.HasWalkedIn();
}
